using AdminAPI.Data;
using AdminAPI.Models;
using Microsoft.EntityFrameworkCore;
using AdminAPI.Features.Permissions.DTOs;

namespace AdminAPI.Features.Permissions.Services;

public class PermissionService : IPermissionService
{
    private readonly AdminDbContext _db;

    public PermissionService(AdminDbContext db)
    {
        _db = db;
    }

    public async Task<PermissionTreeResponse> GetTreeAsync()
    {
        var allPermissions = await _db.Permissions
            .OrderBy(p => p.Sort)
            .ThenBy(p => p.CreatedAt)
            .ToListAsync();

        var trees = BuildTree(allPermissions, null);
        return new PermissionTreeResponse(trees);
    }

    private List<PermissionDto> BuildTree(List<Permission> all, int? parentId)
    {
        var children = all
            .Where(p => p.ParentId == parentId)
            .OrderBy(p => p.Sort)
            .Select(p => new PermissionDto(
                p.Id,
                p.Name,
                p.Code,
                p.Type,
                p.ParentId,
                p.Path,
                p.Icon,
                p.ApiPath,
                p.Sort,
                p.CreatedAt,
                BuildTree(all, p.Id)
            ))
            .ToList();

        return children;
    }

    public async Task<PermissionListResponse> GetListAsync(int page, int pageSize)
    {
        var query = _db.Permissions
            .OrderByDescending(p => p.CreatedAt);

        var total = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new PermissionDto(
                p.Id,
                p.Name,
                p.Code,
                p.Type,
                p.ParentId,
                p.Path,
                p.Icon,
                p.ApiPath,
                p.Sort,
                p.CreatedAt,
                new List<PermissionDto>()
            ))
            .ToListAsync();

        return new PermissionListResponse(items, total);
    }

    public async Task<PermissionDto?> GetByIdAsync(int id)
    {
        var permission = await _db.Permissions.FindAsync(id);
        if (permission == null) return null;

        return new PermissionDto(
            permission.Id,
            permission.Name,
            permission.Code,
            permission.Type,
            permission.ParentId,
            permission.Path,
            permission.Icon,
            permission.ApiPath,
            permission.Sort,
            permission.CreatedAt,
            new List<PermissionDto>()
        );
    }

    public async Task<PermissionDto> CreateAsync(CreatePermissionRequest request)
    {
        var permission = new Permission
        {
            Name = request.Name,
            Code = request.Code,
            Type = request.Type,
            ParentId = request.ParentId,
            Path = request.Path,
            Icon = request.Icon,
            ApiPath = request.ApiPath,
            Sort = request.Sort
        };

        _db.Permissions.Add(permission);
        await _db.SaveChangesAsync();

        return new PermissionDto(
            permission.Id,
            permission.Name,
            permission.Code,
            permission.Type,
            permission.ParentId,
            permission.Path,
            permission.Icon,
            permission.ApiPath,
            permission.Sort,
            permission.CreatedAt,
            new List<PermissionDto>()
        );
    }

    public async Task<PermissionDto?> UpdateAsync(int id, UpdatePermissionRequest request)
    {
        var permission = await _db.Permissions.FindAsync(id);
        if (permission == null) return null;

        if (request.Name != null) permission.Name = request.Name;
        if (request.Code != null) permission.Code = request.Code;
        if (request.Type != null) permission.Type = request.Type.Value;
        if (request.ParentId != null) permission.ParentId = request.ParentId;
        if (request.Path != null) permission.Path = request.Path;
        if (request.Icon != null) permission.Icon = request.Icon;
        if (request.ApiPath != null) permission.ApiPath = request.ApiPath;
        if (request.Sort != null) permission.Sort = request.Sort.Value;

        await _db.SaveChangesAsync();

        return new PermissionDto(
            permission.Id,
            permission.Name,
            permission.Code,
            permission.Type,
            permission.ParentId,
            permission.Path,
            permission.Icon,
            permission.ApiPath,
            permission.Sort,
            permission.CreatedAt,
            new List<PermissionDto>()
        );
    }

    public async Task<bool> DeleteAsync(int id)
    {
        // Check if has children
        var hasChildren = await _db.Permissions.AnyAsync(p => p.ParentId == id);
        if (hasChildren) return false;

        // Check if used by any role
        var isUsed = await _db.RolePermissions.AnyAsync(rp => rp.PermissionId == id);
        if (isUsed) return false;

        var permission = await _db.Permissions.FindAsync(id);
        if (permission == null) return false;

        _db.Permissions.Remove(permission);
        await _db.SaveChangesAsync();
        return true;
    }
}
