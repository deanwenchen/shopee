namespace AdminAPI.Features.Roles.Services;

public interface IRoleService
{
    Task<RoleListResponse> GetListAsync(int page, int pageSize);
    Task<RoleDto?> GetByIdAsync(int id);
    Task<RoleDto> CreateAsync(CreateRoleRequest request);
    Task<RoleDto?> UpdateAsync(int id, UpdateRoleRequest request);
    Task<bool> DeleteAsync(int id);
    Task<bool> AssignPermissionsAsync(int roleId, List<int> permissionIds);
}
