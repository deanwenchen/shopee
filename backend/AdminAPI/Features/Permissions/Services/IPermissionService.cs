using AdminAPI.Features.Permissions.DTOs;

namespace AdminAPI.Features.Permissions.Services;

public interface IPermissionService
{
    Task<PermissionTreeResponse> GetTreeAsync();
    Task<PermissionListResponse> GetListAsync(int page, int pageSize);
    Task<PermissionDto?> GetByIdAsync(int id);
    Task<PermissionDto> CreateAsync(CreatePermissionRequest request);
    Task<PermissionDto?> UpdateAsync(int id, UpdatePermissionRequest request);
    Task<bool> DeleteAsync(int id);
}
