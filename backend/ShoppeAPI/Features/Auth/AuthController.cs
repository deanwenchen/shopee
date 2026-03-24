using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppeAPI.Features.Auth.DTOs;
using ShoppeAPI.Features.Auth;
using Microsoft.Extensions.Configuration;

namespace ShoppeAPI.Features.Auth;

[ApiController]
[Route("api/auth")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IJwtService _jwtService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        IAuthService authService,
        IJwtService jwtService,
        ILogger<AuthController> logger)
    {
        _authService = authService;
        _jwtService = jwtService;
        _logger = logger;
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                success = false,
                message = "Invalid input",
                errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray()
            });
        }

        var (success, userId, message) = await _authService.RegisterAsync(dto);

        if (!success)
        {
            return Conflict(new { success = false, message });
        }

        return Ok(new { success = true, message, userId });
    }

    /// <summary>
    /// Login Step 1 - Verify email exists
    /// </summary>
    [HttpPost("login-step1")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginStep1([FromBody] LoginStep1Dto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { success = false, message = "Invalid email format" });
        }

        var (success, requiresPassword, message) = await _authService.LoginStep1Async(dto.Email);

        return Ok(new { success = true, requiresPassword = true, message = "Please enter your password" });
    }

    /// <summary>
    /// Login Step 2 - Verify password and get tokens
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { success = false, message = "Invalid input" });
        }

        var (success, user, accessToken, message) = await _authService.LoginAsync(dto);

        if (!success || user == null || accessToken == null)
        {
            return Unauthorized(new { success = false, message });
        }

        // Generate and store refresh token
        var refreshToken = await _authService.CreateRefreshTokenAsync(user.Id);

        var jwtSettings = HttpContext.RequestServices.GetRequiredService<IConfiguration>()
            .GetSection("JwtSettings");
        var accessTokenExpiration = jwtSettings.GetValue<int>("AccessTokenExpirationMinutes", 10080);

        // Set refresh token cookie
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = false, // Allow non-HTTPS in development
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(30),
            Path = "/api/auth"
        };

        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);

        return Ok(new
        {
            success = true,
            message,
            user = new { user.Id, user.Email },
            accessToken,
            expiresIn = accessTokenExpiration * 60 // Convert to seconds
        });
    }

    /// <summary>
    /// Logout user - revoke refresh tokens
    /// </summary>
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var userId = GetUserIdFromToken();

        if (userId == null)
        {
            return Unauthorized(new { success = false, message = "Invalid token" });
        }

        await _authService.LogoutAsync(userId.Value);

        // Clear refresh token cookie
        Response.Cookies.Delete("refreshToken");

        return Ok(new { success = true, message = "Logged out successfully" });
    }

    /// <summary>
    /// Refresh access token using refresh token
    /// </summary>
    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(refreshToken))
        {
            return Unauthorized(new { success = false, message = "Refresh token not found" });
        }

        var (success, newAccessToken, newRefreshToken, message) =
            await _authService.RefreshTokenAsync(refreshToken);

        if (!success)
        {
            Response.Cookies.Delete("refreshToken", new CookieOptions { Path = "/api/auth" });
            return Unauthorized(new { success = false, message });
        }

        // Set new refresh token cookie
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(30),
            Path = "/api/auth"
        };

        Response.Cookies.Append("refreshToken", newRefreshToken!, cookieOptions);

        var jwtSettings = HttpContext.RequestServices.GetRequiredService<IConfiguration>()
            .GetSection("JwtSettings");
        var accessTokenExpiration = jwtSettings.GetValue<int>("AccessTokenExpirationMinutes", 10080);

        return Ok(new
        {
            success = true,
            accessToken = newAccessToken,
            expiresIn = accessTokenExpiration * 60
        });
    }

    /// <summary>
    /// Initiate password recovery
    /// </summary>
    [HttpPost("password-recovery")]
    [AllowAnonymous]
    public async Task<IActionResult> PasswordRecovery([FromBody] PasswordRecoveryDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { success = false, message = "Invalid input" });
        }

        var (success, codeId, message) = await _authService.PasswordRecoveryAsync(dto);

        return Ok(new { success = true, codeId, message });
    }

    /// <summary>
    /// Verify recovery code
    /// </summary>
    [HttpPost("verify-code")]
    [AllowAnonymous]
    public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { success = false, message = "Invalid input" });
        }

        var (success, resetToken, message) = await _authService.VerifyCodeAsync(dto.CodeId, dto.Code);

        if (!success)
        {
            return BadRequest(new { success = false, message });
        }

        return Ok(new { success = true, resetToken, message });
    }

    /// <summary>
    /// Reset password with reset token
    /// </summary>
    [HttpPost("reset-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                success = false,
                message = "Invalid input",
                errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray()
            });
        }

        var (success, message) = await _authService.ResetPasswordAsync(dto.ResetToken, dto.NewPassword);

        if (!success)
        {
            return BadRequest(new { success = false, message });
        }

        return Ok(new { success = true, message });
    }

    /// <summary>
    /// Verify current access token
    /// </summary>
    [HttpGet("verify")]
    [Authorize]
    public async Task<IActionResult> Verify()
    {
        var userId = GetUserIdFromToken();

        if (userId == null)
        {
            return Unauthorized(new { valid = false, message = "Invalid token" });
        }

        var user = await _authService.GetUserByIdAsync(userId.Value);

        if (user == null)
        {
            return NotFound(new { valid = false, message = "User not found" });
        }

        return Ok(new
        {
            valid = true,
            user = new { user.Id, user.Email, user.PhoneNumber }
        });
    }

    private int? GetUserIdFromToken()
    {
        var userIdClaim = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (int.TryParse(userIdClaim, out var userId))
        {
            return userId;
        }

        return null;
    }
}
