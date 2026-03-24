using ShoppeAPI.Models;
using ShoppeAPI.Features.Auth.DTOs;

namespace ShoppeAPI.Features.Auth;

public interface IJwtService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    DateTime GetRefreshTokenExpiration();
}

public interface IUserService
{
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
    Task<User> CreateAsync(string email, string passwordHash, string? phoneNumber);
    Task UpdateLastLoginAsync(User user);
}

public interface IAuthService
{
    Task<(bool Success, int? UserId, string? Message)> RegisterAsync(RegisterDto dto);
    Task<(bool Success, bool RequiresPassword, string? Message)> LoginStep1Async(string email);
    Task<(bool Success, User? User, string? AccessToken, string? Message)> LoginAsync(LoginDto dto);
    Task<bool> LogoutAsync(int userId);
    Task<string> CreateRefreshTokenAsync(int userId);
    Task<(bool Success, string? NewAccessToken, string? NewRefreshToken, string? Message)> RefreshTokenAsync(string refreshToken);
    Task<(bool Success, string? CodeId, string? Message)> PasswordRecoveryAsync(PasswordRecoveryDto dto);
    Task<(bool Success, string? ResetToken, string? Message)> VerifyCodeAsync(string codeId, string code);
    Task<(bool Success, string? Message)> ResetPasswordAsync(string resetToken, string newPassword);
    Task<User?> GetUserByIdAsync(int userId);
    Task<User?> GetUserByEmailAsync(string email);
}
