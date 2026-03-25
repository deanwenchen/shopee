using System.ComponentModel.DataAnnotations;

namespace AdminAPI.Features.Admins;

public record CreateAdminRequest(
    [Required][MaxLength(50)] string Username,
    [Required][MaxLength(50)] string Password,
    [MaxLength(100)] string? Email,
    [MaxLength(20)] string? Phone,
    List<int> RoleIds
);

public record UpdateAdminRequest(
    [MaxLength(100)] string? Email,
    [MaxLength(20)] string? Phone,
    List<int> RoleIds
);

public record AdminDto(
    int Id,
    string Username,
    string? Email,
    string? Phone,
    int Status,
    DateTime CreatedAt,
    DateTime? LastLoginAt,
    List<int> RoleIds
);

public record AdminListResponse(
    List<AdminDto> Items,
    int Total
);

public record UpdateStatusRequest(int Status);

public record ResetPasswordRequest(string NewPassword);
