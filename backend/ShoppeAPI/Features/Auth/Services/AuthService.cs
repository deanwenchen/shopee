using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using ShoppeAPI.Data;
using ShoppeAPI.Models;
using ShoppeAPI.Features.Auth.DTOs;

namespace ShoppeAPI.Features.Auth;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    // In-memory store for password recovery codes (use Redis in production)
    private static readonly Dictionary<string, (string Code, string Email, DateTime ExpiresAt)> _recoveryCodes
        = new();

    public AuthService(
        AppDbContext context,
        IJwtService jwtService,
        IUserService userService,
        IConfiguration configuration)
    {
        _context = context;
        _jwtService = jwtService;
        _userService = userService;
        _configuration = configuration;
    }

    public async Task<(bool Success, int? UserId, string? Message)> RegisterAsync(RegisterDto dto)
    {
        // Check if email already exists
        if (await _userService.EmailExistsAsync(dto.Email))
        {
            return (false, null, "Email already registered");
        }

        // Hash password with BCrypt
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password, 10);

        // Create user
        var user = await _userService.CreateAsync(dto.Email, passwordHash, dto.PhoneNumber);

        return (true, user.Id, "Registration successful");
    }

    public async Task<(bool Success, bool RequiresPassword, string? Message)> LoginStep1Async(string email)
    {
        var user = await _userService.GetByEmailAsync(email);

        // Always return requiresPassword=true for security (don't reveal if email exists)
        if (user == null)
        {
            // Simulate delay to prevent timing attacks
            await Task.Delay(100);
            return (true, true, "Please enter your password");
        }

        return (true, true, "Please enter your password");
    }

    public async Task<(bool Success, User? User, string? AccessToken, string? Message)> LoginAsync(LoginDto dto)
    {
        var user = await _userService.GetByEmailAsync(dto.Email);

        if (user == null)
        {
            return (false, null, null, "Invalid email or password");
        }

        // Verify password
        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            return (false, null, null, "Invalid email or password");
        }

        // Generate tokens
        var accessToken = _jwtService.GenerateAccessToken(user);

        // Update last login
        await _userService.UpdateLastLoginAsync(user);

        return (true, user, accessToken, "Login successful");
    }

    public async Task<bool> LogoutAsync(int userId)
    {
        // Revoke all refresh tokens for the user
        var tokens = await _context.RefreshTokens
            .Where(rt => rt.UserId == userId && !rt.IsRevoked)
            .ToListAsync();

        foreach (var token in tokens)
        {
            token.IsRevoked = true;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<string> CreateRefreshTokenAsync(int userId)
    {
        var refreshToken = _jwtService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            UserId = userId,
            Token = refreshToken,
            ExpiresAt = _jwtService.GetRefreshTokenExpiration(),
            CreatedAt = DateTime.UtcNow
        };

        _context.RefreshTokens.Add(refreshTokenEntity);
        await _context.SaveChangesAsync();

        return refreshToken;
    }

    public async Task<(bool Success, string? NewAccessToken, string? NewRefreshToken, string? Message)> RefreshTokenAsync(
        string refreshToken)
    {
        var token = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

        if (token == null)
        {
            return (false, null, null, "Invalid refresh token");
        }

        if (token.IsUsed || token.IsRevoked)
        {
            // Potential token reuse attack - revoke all tokens for this user
            var allTokens = await _context.RefreshTokens.Where(rt => rt.UserId == token.UserId).ToListAsync();
            foreach (var t in allTokens)
            {
                t.IsRevoked = true;
            }
            await _context.SaveChangesAsync();
            return (false, null, null, "Refresh token has been revoked");
        }

        if (token.ExpiresAt < DateTime.UtcNow)
        {
            token.IsRevoked = true;
            await _context.SaveChangesAsync();
            return (false, null, null, "Refresh token has expired");
        }

        // Mark token as used
        token.IsUsed = true;

        // Generate new access token
        var user = await _userService.GetByIdAsync(token.UserId);
        if (user == null)
        {
            return (false, null, null, "User not found");
        }

        var newAccessToken = _jwtService.GenerateAccessToken(user);

        // Generate new refresh token
        var newRefreshToken = _jwtService.GenerateRefreshToken();
        var newRefreshTokenEntity = new RefreshToken
        {
            UserId = token.UserId,
            Token = newRefreshToken,
            ExpiresAt = _jwtService.GetRefreshTokenExpiration(),
            CreatedAt = DateTime.UtcNow
        };

        _context.RefreshTokens.Add(newRefreshTokenEntity);
        await _context.SaveChangesAsync();

        return (true, newAccessToken, newRefreshToken, null);
    }

    public async Task<(bool Success, string? CodeId, string? Message)> PasswordRecoveryAsync(PasswordRecoveryDto dto)
    {
        var user = await _userService.GetByEmailAsync(dto.Email);

        // Always return success for security (don't reveal if email exists)
        if (user == null)
        {
            await Task.Delay(100); // Prevent timing attacks
            return (true, Guid.NewGuid().ToString("N"), "If the email exists, a verification code has been sent");
        }

        // Generate 4-digit code
        var code = RandomNumberGenerator.GetInt32(1000, 9999).ToString();
        var codeId = Guid.NewGuid().ToString("N");

        // Store code (in production, send via SMS/Email and store in Redis)
        _recoveryCodes[codeId] = (code, user.Email, DateTime.UtcNow.AddMinutes(10));

        // TODO: Send code via SMS/Email service
        // For development, log the code
        Console.WriteLine($"[DEV] Password recovery code for {user.Email}: {code}");

        return (true, codeId, "Verification code sent");
    }

    public async Task<(bool Success, string? ResetToken, string? Message)> VerifyCodeAsync(string codeId, string code)
    {
        if (!_recoveryCodes.ContainsKey(codeId))
        {
            return (false, null, "Invalid code ID");
        }

        var (storedCode, email, expiresAt) = _recoveryCodes[codeId];

        if (expiresAt < DateTime.UtcNow)
        {
            _recoveryCodes.Remove(codeId);
            return (false, null, "Code has expired");
        }

        if (storedCode != code)
        {
            return (false, null, "Invalid code");
        }

        // Generate reset token
        var user = await _userService.GetByEmailAsync(email);
        if (user == null)
        {
            return (false, null, "User not found");
        }

        var resetToken = _jwtService.GenerateRefreshToken();

        // Store reset token with expiration (15 minutes)
        var resetTokenEntity = new RefreshToken
        {
            UserId = user.Id,
            Token = resetToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(15),
            CreatedAt = DateTime.UtcNow,
            IsUsed = false
        };

        _context.RefreshTokens.Add(resetTokenEntity);
        await _context.SaveChangesAsync();

        // Remove used code
        _recoveryCodes.Remove(codeId);

        return (true, resetToken, "Code verified successfully");
    }

    public async Task<(bool Success, string? Message)> ResetPasswordAsync(string resetToken, string newPassword)
    {
        var tokenEntity = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == resetToken);

        if (tokenEntity == null || tokenEntity.IsUsed || tokenEntity.IsRevoked)
        {
            return (false, "Invalid or expired reset token");
        }

        if (tokenEntity.ExpiresAt < DateTime.UtcNow)
        {
            tokenEntity.IsRevoked = true;
            await _context.SaveChangesAsync();
            return (false, "Reset token has expired");
        }

        var user = await _userService.GetByIdAsync(tokenEntity.UserId);
        if (user == null)
        {
            return (false, "User not found");
        }

        // Update password
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword, 10);
        user.UpdatedAt = DateTime.UtcNow;

        // Mark reset token as used
        tokenEntity.IsUsed = true;

        await _context.SaveChangesAsync();

        return (true, "Password reset successfully");
    }

    public Task<User?> GetUserByIdAsync(int userId) => _userService.GetByIdAsync(userId);
    public Task<User?> GetUserByEmailAsync(string email) => _userService.GetByEmailAsync(email);
}
