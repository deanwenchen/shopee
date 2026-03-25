using AdminAPI.Data;
using AdminAPI.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace AdminAPI.Features.Auth.Services;

public class AuthService : IAuthService
{
    private readonly AdminDbContext _db;
    private readonly IJwtService _jwtService;

    public AuthService(AdminDbContext db, IJwtService jwtService)
    {
        _db = db;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse?> LoginAsync(string username, string password)
    {
        var admin = await _db.AdminUsers
            .Include(a => a.AdminRoles)
                .ThenInclude(ar => ar.Role)
                    .ThenInclude(r => r.RolePermissions)
                        .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(a => a.Username == username);

        if (admin == null || admin.Status != 1)
            return null;

        if (!BCrypt.Net.BCrypt.Verify(password, admin.PasswordHash))
            return null;

        var permissions = admin.AdminRoles
            .SelectMany(ar => ar.Role.RolePermissions)
            .Select(rp => rp.Permission.Code)
            .Distinct()
            .ToList();

        admin.LastLoginAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        var token = _jwtService.GenerateToken(admin.Id, admin.Username, permissions);
        var refreshToken = _jwtService.GenerateRefreshToken();

        return new LoginResponse(
            admin.Id,
            admin.Username,
            token,
            refreshToken,
            permissions
        );
    }

    public Task<bool> LogoutAsync(int adminId)
    {
        return Task.FromResult(true);
    }

    public async Task<LoginResponse?> RefreshTokenAsync(string refreshToken)
    {
        // TODO: Implement refresh token validation
        return null;
    }
}
