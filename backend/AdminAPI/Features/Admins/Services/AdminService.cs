using AdminAPI.Data;
using AdminAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminAPI.Features.Admins.Services;

public class AdminService : IAdminService
{
    private readonly AdminDbContext _db;

    public AdminService(AdminDbContext db)
    {
        _db = db;
    }

    public async Task<AdminListResponse> GetListAsync(int page, int pageSize)
    {
        var query = _db.AdminUsers
            .Include(a => a.AdminRoles)
            .OrderByDescending(a => a.CreatedAt);

        var total = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(a => new AdminDto(
                a.Id,
                a.Username,
                a.Email,
                a.Phone,
                a.Status,
                a.CreatedAt,
                a.LastLoginAt,
                a.AdminRoles.Select(ar => ar.RoleId).ToList()
            ))
            .ToListAsync();

        return new AdminListResponse(items, total);
    }

    public async Task<AdminDto?> GetByIdAsync(int id)
    {
        var admin = await _db.AdminUsers
            .Include(a => a.AdminRoles)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (admin == null) return null;

        return new AdminDto(
            admin.Id,
            admin.Username,
            admin.Email,
            admin.Phone,
            admin.Status,
            admin.CreatedAt,
            admin.LastLoginAt,
            admin.AdminRoles.Select(ar => ar.RoleId).ToList()
        );
    }

    public async Task<AdminDto> CreateAsync(CreateAdminRequest request)
    {
        var admin = new AdminUser
        {
            Username = request.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Email = request.Email,
            Phone = request.Phone,
            Status = 1
        };

        if (request.RoleIds.Any())
        {
            admin.AdminRoles = request.RoleIds.Select(roleId =>
                new AdminRole { AdminId = admin.Id, RoleId = roleId }
            ).ToList();
        }

        _db.AdminUsers.Add(admin);
        await _db.SaveChangesAsync();

        return new AdminDto(
            admin.Id,
            admin.Username,
            admin.Email,
            admin.Phone,
            admin.Status,
            admin.CreatedAt,
            admin.LastLoginAt,
            admin.AdminRoles.Select(ar => ar.RoleId).ToList()
        );
    }

    public async Task<AdminDto?> UpdateAsync(int id, UpdateAdminRequest request)
    {
        var admin = await _db.AdminUsers
            .Include(a => a.AdminRoles)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (admin == null) return null;

        admin.Email = request.Email;
        admin.Phone = request.Phone;

        // Update roles
        _db.AdminRoles.RemoveRange(admin.AdminRoles);
        admin.AdminRoles = request.RoleIds.Select(roleId =>
            new AdminRole { AdminId = admin.Id, RoleId = roleId }
        ).ToList();

        await _db.SaveChangesAsync();

        return new AdminDto(
            admin.Id,
            admin.Username,
            admin.Email,
            admin.Phone,
            admin.Status,
            admin.CreatedAt,
            admin.LastLoginAt,
            admin.AdminRoles.Select(ar => ar.RoleId).ToList()
        );
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var admin = await _db.AdminUsers.FindAsync(id);
        if (admin == null) return false;

        _db.AdminUsers.Remove(admin);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateStatusAsync(int id, int status)
    {
        var admin = await _db.AdminUsers.FindAsync(id);
        if (admin == null) return false;

        admin.Status = status;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ResetPasswordAsync(int adminId, int targetAdminId, string newPassword)
    {
        var admin = await _db.AdminUsers.FindAsync(targetAdminId);
        if (admin == null) return false;

        admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        await _db.SaveChangesAsync();
        return true;
    }
}
