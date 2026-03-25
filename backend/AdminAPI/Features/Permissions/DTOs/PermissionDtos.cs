using System.ComponentModel.DataAnnotations;

namespace AdminAPI.Features.Permissions.DTOs;

public record CreatePermissionRequest(
    [Required][MaxLength(100)] string Name,
    [Required][MaxLength(100)] string Code,
    int Type, // 1=Directory, 2=Menu, 3=Button, 4=API
    int? ParentId,
    [MaxLength(255)] string? Path,
    [MaxLength(100)] string? Icon,
    [MaxLength(255)] string? ApiPath,
    int Sort = 0
);

public record UpdatePermissionRequest(
    [MaxLength(100)] string? Name,
    [MaxLength(100)] string? Code,
    int? Type,
    int? ParentId,
    [MaxLength(255)] string? Path,
    [MaxLength(100)] string? Icon,
    [MaxLength(255)] string? ApiPath,
    int? Sort
);

public record PermissionDto(
    int Id,
    string Name,
    string Code,
    int Type,
    int? ParentId,
    string? Path,
    string? Icon,
    string? ApiPath,
    int Sort,
    DateTime CreatedAt,
    List<PermissionDto> Children
);

public record PermissionTreeResponse(
    List<PermissionDto> Trees
);

public record PermissionListResponse(
    List<PermissionDto> Items,
    int Total
);
