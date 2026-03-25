using AdminAPI.Data;
using AdminAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminAPI.Features.Roles.Services;

public class RoleService : IRoleService
{
    private readonly AdminDbContext _db;

    public RoleService(AdminDbContext db)
    {
        _db = db;
    }

    public async Task<RoleListResponse> GetListAsync(int page, int pageSize)
    {
        var query = _db.Roles
            .Include(r => r.RolePermissions)
            .OrderByDescending(r => r.CreatedAt);

        var total = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(r => new RoleDto(
                r.Id,
                r.Name,
                r.Description,
                r.CreatedAt,
                r.RolePermissions.Select(rp => rp.PermissionId).ToList()
            ))
            .ToListAsync();

        return new RoleListResponse(items, total);
    }

    public async Task<RoleDto?> GetByIdAsync(int id)
    {
        var role = await _db.Roles
            .Include(r => r.RolePermissions)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (role == null) return null;

        return new RoleDto(
            role.Id,
            role.Name,
            role.Description,
            role.CreatedAt,
            role.RolePermissions.Select(rp => rp.PermissionId).ToList()
        );
    }

    public async Task<RoleDto> CreateAsync(CreateRoleRequest request)
    {
        var role = new Role
        {
            Name = request.Name,
            Description = request.Description
        };

        if (request.PermissionIds.Any())
        {
            role.RolePermissions = request.PermissionIds.Select(permissionId =>
                new RolePermission { RoleId = role.Id, PermissionId = permissionId }
            ).ToList();
        }

        _db.Roles.Add(role);
        await _db.SaveChangesAsync();

        return new RoleDto(
            role.Id,
            role.Name,
            role.Description,
            role.CreatedAt,
            role.RolePermissions.Select(rp => rp.PermissionId).ToList()
        );
    }

    public async Task<RoleDto?> UpdateAsync(int id, UpdateRoleRequest request)
    {
        var role = await _db.Roles
            .Include(r => r.RolePermissions)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (role == null) return null;

        if (request.Name != null) role.Name = request.Name;
        if (request.Description != null) role.Description = request.Description;

        // Update permissions
        _db.RolePermissions.RemoveRange(role.RolePermissions);
        if (request.PermissionIds.Any())
        {
            role.RolePermissions = request.PermissionIds.Select(permissionId =>
                new RolePermission { RoleId = role.Id, PermissionId = permissionId }
            ).ToList();
        }

        await _db.SaveChangesAsync();

        return new RoleDto(
            role.Id,
            role.Name,
            role.Description,
            role.CreatedAt,
            role.RolePermissions.Select(rp => rp.PermissionId).ToList()
        );
    }

    public async Task<bool> DeleteAsync(int id)
    {
        // Check if role is assigned to any admin
        var isAssigned = await _db.AdminRoles.AnyAsync(ar => ar.RoleId == id);
        if (isAssigned) return false;

        var role = await _db.Roles.FindAsync(id);
        if (role == null) return false;

        _db.Roles.Remove(role);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AssignPermissionsAsync(int roleId, List<int> permissionIds)
    {
        var role = await _db.Roles
            .Include(r => r.RolePermissions)
            .FirstOrDefaultAsync(r => r.Id == roleId);

        if (role == null) return false;

        _db.RolePermissions.RemoveRange(role.RolePermissions);
        role.RolePermissions = permissionIds.Select(permissionId =>
            new RolePermission { RoleId = role.Id, PermissionId = permissionId }
        ).ToList();

        await _db.SaveChangesAsync();
        return true;
    }
}
