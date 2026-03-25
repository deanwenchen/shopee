using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AdminAPI.Data;
using AdminAPI.Models;
using BCrypt.Net;

namespace AdminAPI.Database;

/// <summary>
/// Database Seeder - Initializes database and seeds essential data
/// Can be run as a standalone program or integrated into startup
/// </summary>
public class DbSeeder
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DbSeeder> _logger;

    public DbSeeder(IServiceProvider serviceProvider, ILogger<DbSeeder> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    /// <summary>
    /// Main entry point for database initialization
    /// </summary>
    public async Task InitializeDatabaseAsync()
    {
        _logger.LogInformation("Starting database initialization...");

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AdminDbContext>();

        try
        {
            // Ensure database exists
            _logger.LogInformation("Checking database existence...");
            await context.Database.EnsureCreatedAsync();
            _logger.LogInformation("Database check completed.");

            // Check if data already exists
            if (await context.AdminUsers.AnyAsync())
            {
                _logger.LogInformation("Database already initialized. Skipping seed data.");
                return;
            }

            _logger.LogInformation("Seeding initial data...");
            await SeedDataAsync(context);
            _logger.LogInformation("Database initialization completed successfully!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    /// <summary>
    /// Seeds the database with initial data
    /// </summary>
    private async Task SeedDataAsync(AdminDbContext context)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            // 1. Create default admin user
            var adminUser = await CreateDefaultAdminAsync(context);
            _logger.LogInformation("Created default admin user: {Username}", adminUser.Username);

            // 2. Create base permissions
            var permissions = await CreatePermissionsAsync(context);
            _logger.LogInformation("Created {Count} base permissions", permissions.Count);

            // 3. Create Super Admin role
            var superAdminRole = await CreateSuperAdminRoleAsync(context);
            _logger.LogInformation("Created Super Admin role");

            // 4. Assign all permissions to Super Admin role
            await AssignPermissionsToRoleAsync(context, superAdminRole.Id, permissions);
            _logger.LogInformation("Assigned {Count} permissions to Super Admin role", permissions.Count);

            // 5. Assign Super Admin role to admin user
            await AssignRoleToAdminAsync(context, adminUser.Id, superAdminRole.Id);
            _logger.LogInformation("Assigned Super Admin role to admin user");

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// Creates the default admin user
    /// Username: admin, Password: Admin@123
    /// </summary>
    private async Task<AdminUser> CreateDefaultAdminAsync(AdminDbContext context)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123", 10);

        var adminUser = new AdminUser
        {
            Username = "admin",
            PasswordHash = passwordHash,
            Email = "admin@shoppe.com",
            Phone = null,
            Status = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        context.AdminUsers.Add(adminUser);
        await context.SaveChangesAsync();

        return adminUser;
    }

    /// <summary>
    /// Creates the hierarchical permission structure
    /// </summary>
    private async Task<List<Permission>> CreatePermissionsAsync(AdminDbContext context)
    {
        var permissions = new List<Permission>();
        var now = DateTime.UtcNow;

        // Level 1: Directories (type=1)
        var directories = new[]
        {
            new { Name = "Dashboard", Code = "dashboard", Path = "/dashboard", Icon = "Dashboard", Sort = 1 },
            new { Name = "System Management", Code = "system", Path = "/system", Icon = "Settings", Sort = 2 },
            new { Name = "User Management", Code = "user", Path = "/users", Icon = "User", Sort = 3 },
            new { Name = "Logs", Code = "logs", Path = "/logs", Icon = "Document", Sort = 4 }
        };

        foreach (var dir in directories)
        {
            var permission = new Permission
            {
                Name = dir.Name,
                Code = dir.Code,
                Type = 1,
                ParentId = null,
                Path = dir.Path,
                Icon = dir.Icon,
                Sort = dir.Sort,
                CreatedAt = now
            };
            permissions.Add(permission);
        }

        await context.SaveChangesAsync();

        // Level 2: Menus (type=2)
        var menus = new[]
        {
            new { Name = "Dashboard Home", Code = "dashboard:home", ParentCode = "dashboard", Path = "/dashboard", Icon = "Home", Sort = 1 },
            new { Name = "Admin Management", Code = "admin", ParentCode = "system", Path = "/system/admins", Icon = "User", Sort = 1 },
            new { Name = "Role Management", Code = "role", ParentCode = "system", Path = "/system/roles", Icon = "Lock", Sort = 2 },
            new { Name = "Permission Management", Code = "permission", ParentCode = "system", Path = "/system/permissions", Icon = "Key", Sort = 3 },
            new { Name = "User List", Code = "user:list-page", ParentCode = "user", Path = "/users", Icon = "Users", Sort = 1 },
            new { Name = "Operation Logs", Code = "logs:operation", ParentCode = "logs", Path = "/logs", Icon = "History", Sort = 1 }
        };

        foreach (var menu in menus)
        {
            var parent = permissions.First(p => p.Code == menu.ParentCode);
            var permission = new Permission
            {
                Name = menu.Name,
                Code = menu.Code,
                Type = 2,
                ParentId = parent.Id,
                Path = menu.Path,
                Icon = menu.Icon,
                Sort = menu.Sort,
                CreatedAt = now
            };
            permissions.Add(permission);
        }

        await context.SaveChangesAsync();

        // Level 3: Buttons (type=3) - Admin Management
        var adminButtons = new[]
        {
            new { Name = "Admin Create", Code = "admin:create", ParentCode = "admin", Sort = 1 },
            new { Name = "Admin Edit", Code = "admin:update", ParentCode = "admin", Sort = 2 },
            new { Name = "Admin Delete", Code = "admin:delete", ParentCode = "admin", Sort = 3 },
            new { Name = "Admin Status", Code = "admin:status", ParentCode = "admin", Sort = 4 },
            new { Name = "Admin Reset Password", Code = "admin:reset-pwd", ParentCode = "admin", Sort = 5 },
            new { Name = "Admin Detail", Code = "admin:detail", ParentCode = "admin", Sort = 6 }
        };

        foreach (var btn in adminButtons)
        {
            var parent = permissions.First(p => p.Code == btn.ParentCode);
            permissions.Add(new Permission
            {
                Name = btn.Name,
                Code = btn.Code,
                Type = 3,
                ParentId = parent.Id,
                Sort = btn.Sort,
                CreatedAt = now
            });
        }

        // Level 3: Buttons - Role Management
        var roleButtons = new[]
        {
            new { Name = "Role Create", Code = "role:create", ParentCode = "role", Sort = 1 },
            new { Name = "Role Edit", Code = "role:update", ParentCode = "role", Sort = 2 },
            new { Name = "Role Delete", Code = "role:delete", ParentCode = "role", Sort = 3 },
            new { Name = "Role Assign Permissions", Code = "role:assign", ParentCode = "role", Sort = 4 },
            new { Name = "Role Detail", Code = "role:detail", ParentCode = "role", Sort = 5 }
        };

        foreach (var btn in roleButtons)
        {
            var parent = permissions.First(p => p.Code == btn.ParentCode);
            permissions.Add(new Permission
            {
                Name = btn.Name,
                Code = btn.Code,
                Type = 3,
                ParentId = parent.Id,
                Sort = btn.Sort,
                CreatedAt = now
            });
        }

        // Level 3: Buttons - Permission Management
        var permissionButtons = new[]
        {
            new { Name = "Permission Create", Code = "permission:create", ParentCode = "permission", Sort = 1 },
            new { Name = "Permission Edit", Code = "permission:update", ParentCode = "permission", Sort = 2 },
            new { Name = "Permission Delete", Code = "permission:delete", ParentCode = "permission", Sort = 3 },
            new { Name = "Permission Detail", Code = "permission:detail", ParentCode = "permission", Sort = 4 }
        };

        foreach (var btn in permissionButtons)
        {
            var parent = permissions.First(p => p.Code == btn.ParentCode);
            permissions.Add(new Permission
            {
                Name = btn.Name,
                Code = btn.Code,
                Type = 3,
                ParentId = parent.Id,
                Sort = btn.Sort,
                CreatedAt = now
            });
        }

        // Level 3: Buttons - User Management
        var userButtons = new[]
        {
            new { Name = "User Create", Code = "user:create", ParentCode = "user:list-page", Sort = 1 },
            new { Name = "User Edit", Code = "user:update", ParentCode = "user:list-page", Sort = 2 },
            new { Name = "User Delete", Code = "user:delete", ParentCode = "user:list-page", Sort = 3 },
            new { Name = "User Detail", Code = "user:detail", ParentCode = "user:list-page", Sort = 4 }
        };

        foreach (var btn in userButtons)
        {
            var parent = permissions.First(p => p.Code == btn.ParentCode);
            permissions.Add(new Permission
            {
                Name = btn.Name,
                Code = btn.Code,
                Type = 3,
                ParentId = parent.Id,
                Sort = btn.Sort,
                CreatedAt = now
            });
        }

        // Level 3: Buttons - Logs
        var logButtons = new[]
        {
            new { Name = "Log Detail", Code = "logs:detail", ParentCode = "logs:operation", Sort = 1 }
        };

        foreach (var btn in logButtons)
        {
            var parent = permissions.First(p => p.Code == btn.ParentCode);
            permissions.Add(new Permission
            {
                Name = btn.Name,
                Code = btn.Code,
                Type = 3,
                ParentId = parent.Id,
                Sort = btn.Sort,
                CreatedAt = now
            });
        }

        // Level 4: API Permissions (type=4)
        var apiPermissions = new[]
        {
            // Admin APIs
            new { Name = "API: Get Admins", Code = "admin:list", ParentCode = "admin", ApiPath = "GET /api/admin" },
            new { Name = "API: Get Admin", Code = "admin:detail", ParentCode = "admin", ApiPath = "GET /api/admin/{id}" },
            new { Name = "API: Create Admin", Code = "admin:create", ParentCode = "admin", ApiPath = "POST /api/admin" },
            new { Name = "API: Update Admin", Code = "admin:update", ParentCode = "admin", ApiPath = "PUT /api/admin/{id}" },
            new { Name = "API: Delete Admin", Code = "admin:delete", ParentCode = "admin", ApiPath = "DELETE /api/admin/{id}" },
            new { Name = "API: Update Admin Status", Code = "admin:status", ParentCode = "admin", ApiPath = "PUT /api/admin/{id}/status" },
            new { Name = "API: Reset Admin Password", Code = "admin:reset-pwd", ParentCode = "admin", ApiPath = "POST /api/admin/{id}/reset-password" },

            // Role APIs
            new { Name = "API: Get Roles", Code = "role:list", ParentCode = "role", ApiPath = "GET /api/role" },
            new { Name = "API: Get Role", Code = "role:detail", ParentCode = "role", ApiPath = "GET /api/role/{id}" },
            new { Name = "API: Create Role", Code = "role:create", ParentCode = "role", ApiPath = "POST /api/role" },
            new { Name = "API: Update Role", Code = "role:update", ParentCode = "role", ApiPath = "PUT /api/role/{id}" },
            new { Name = "API: Delete Role", Code = "role:delete", ParentCode = "role", ApiPath = "DELETE /api/role/{id}" },
            new { Name = "API: Assign Permissions to Role", Code = "role:assign", ParentCode = "role", ApiPath = "PUT /api/role/{id}/permissions" },

            // Permission APIs
            new { Name = "API: Get Permissions", Code = "permission:list", ParentCode = "permission", ApiPath = "GET /api/permission" },
            new { Name = "API: Get Permission Tree", Code = "permission:tree", ParentCode = "permission", ApiPath = "GET /api/permission/tree" },
            new { Name = "API: Get Permission", Code = "permission:detail", ParentCode = "permission", ApiPath = "GET /api/permission/{id}" },
            new { Name = "API: Create Permission", Code = "permission:create", ParentCode = "permission", ApiPath = "POST /api/permission" },
            new { Name = "API: Update Permission", Code = "permission:update", ParentCode = "permission", ApiPath = "PUT /api/permission/{id}" },
            new { Name = "API: Delete Permission", Code = "permission:delete", ParentCode = "permission", ApiPath = "DELETE /api/permission/{id}" },

            // User APIs
            new { Name = "API: Get Users", Code = "user:list", ParentCode = "user:list-page", ApiPath = "GET /api/user" },
            new { Name = "API: Get User", Code = "user:detail", ParentCode = "user:list-page", ApiPath = "GET /api/user/{id}" },
            new { Name = "API: Create User", Code = "user:create", ParentCode = "user:list-page", ApiPath = "POST /api/user" },
            new { Name = "API: Update User", Code = "user:update", ParentCode = "user:list-page", ApiPath = "PUT /api/user/{id}" },
            new { Name = "API: Delete User", Code = "user:delete", ParentCode = "user:list-page", ApiPath = "DELETE /api/user/{id}" },

            // Log APIs
            new { Name = "API: Get Logs", Code = "logs:list", ParentCode = "logs:operation", ApiPath = "GET /api/log" },
            new { Name = "API: Get Log", Code = "logs:detail", ParentCode = "logs:operation", ApiPath = "GET /api/log/{id}" }
        };

        foreach (var api in apiPermissions)
        {
            var parent = permissions.First(p => p.Code == api.ParentCode);
            permissions.Add(new Permission
            {
                Name = api.Name,
                Code = api.Code,
                Type = 4,
                ParentId = parent.Id,
                ApiPath = api.ApiPath,
                Sort = 0,
                CreatedAt = now
            });
        }

        context.Permissions.AddRange(permissions);
        await context.SaveChangesAsync();

        return permissions;
    }

    /// <summary>
    /// Creates the Super Admin role
    /// </summary>
    private async Task<Role> CreateSuperAdminRoleAsync(AdminDbContext context)
    {
        var role = new Role
        {
            Name = "Super Admin",
            Description = "System administrator with all permissions",
            CreatedAt = DateTime.UtcNow
        };

        context.Roles.Add(role);
        await context.SaveChangesAsync();

        return role;
    }

    /// <summary>
    /// Assigns permissions to a role
    /// </summary>
    private async Task AssignPermissionsToRoleAsync(AdminDbContext context, int roleId, List<Permission> permissions)
    {
        var rolePermissions = permissions.Select(p => new RolePermission
        {
            RoleId = roleId,
            PermissionId = p.Id
        }).ToList();

        context.RolePermissions.AddRange(rolePermissions);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Assigns a role to an admin user
    /// </summary>
    private async Task AssignRoleToAdminAsync(AdminDbContext context, int adminId, int roleId)
    {
        var adminRole = new AdminRole
        {
            AdminId = adminId,
            RoleId = roleId
        };

        context.AdminRoles.Add(adminRole);
        await context.SaveChangesAsync();
    }
}
