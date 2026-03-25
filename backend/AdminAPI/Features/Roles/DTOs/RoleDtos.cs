using System.ComponentModel.DataAnnotations;

namespace AdminAPI.Features.Roles;

public record CreateRoleRequest(
    [Required][MaxLength(50)] string Name,
    [MaxLength(255)] string? Description,
    List<int> PermissionIds
);

public record UpdateRoleRequest(
    [MaxLength(50)] string? Name,
    [MaxLength(255)] string? Description,
    List<int> PermissionIds
);

public record RoleDto(
    int Id,
    string Name,
    string? Description,
    DateTime CreatedAt,
    List<int> PermissionIds
);

public record RoleListResponse(
    List<RoleDto> Items,
    int Total
);
