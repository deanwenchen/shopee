using System.ComponentModel.DataAnnotations;

namespace AdminAPI.Features.Auth;

public record LoginRequest(
    [Required][MaxLength(50)] string Username,
    [Required][MaxLength(50)] string Password
);

public record LoginResponse(
    int AdminId,
    string Username,
    string Token,
    string RefreshToken,
    List<string> Permissions
);

public record RefreshTokenRequest(
    [Required] string RefreshToken
);

public record AuthAdminDto(
    int Id,
    string Username,
    string? Email,
    List<string> Permissions
);
