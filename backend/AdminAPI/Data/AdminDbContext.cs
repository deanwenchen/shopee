using Microsoft.EntityFrameworkCore;
using AdminAPI.Models;

namespace AdminAPI.Data;

/// <summary>
/// AdminDbContext - Entity Framework Core database context for AdminAPI.
/// Provides access to all administrative entities and configurations.
/// </summary>
public class AdminDbContext : DbContext
{
    public AdminDbContext(DbContextOptions<AdminDbContext> options)
        : base(options)
    {
    }

    public DbSet<AdminUser> AdminUsers { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<AdminRole> AdminRoles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<OperationLog> OperationLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // AdminRole composite key
        modelBuilder.Entity<AdminRole>()
            .HasKey(ar => new { ar.AdminId, ar.RoleId });

        // RolePermission composite key
        modelBuilder.Entity<RolePermission>()
            .HasKey(rp => new { rp.RoleId, rp.PermissionId });

        // AdminUser index
        modelBuilder.Entity<AdminUser>()
            .HasIndex(au => au.Username)
            .IsUnique();
    }
}
