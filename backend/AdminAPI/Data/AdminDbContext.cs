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

        // AdminUser configuration
        modelBuilder.Entity<AdminUser>(entity =>
        {
            entity.ToTable("admin_users");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Username).HasColumnName("username");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.LastLoginAt).HasColumnName("last_login_at");
            entity.HasIndex(e => e.Username).IsUnique();
        });

        // Role configuration
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("roles");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
        });

        // Permission configuration
        modelBuilder.Entity<Permission>(entity =>
        {
            entity.ToTable("permissions");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.Path).HasColumnName("path");
            entity.Property(e => e.Icon).HasColumnName("icon");
            entity.Property(e => e.ApiPath).HasColumnName("api_path");
            entity.Property(e => e.Sort).HasColumnName("sort");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
        });

        // AdminRole composite key
        modelBuilder.Entity<AdminRole>(entity =>
        {
            entity.ToTable("admin_roles");
            entity.HasKey(ar => new { ar.AdminId, ar.RoleId });
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
        });

        // RolePermission composite key
        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.ToTable("role_permissions");
            entity.HasKey(rp => new { rp.RoleId, rp.PermissionId });
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
        });

        // OperationLog configuration
        modelBuilder.Entity<OperationLog>(entity =>
        {
            entity.ToTable("operation_logs");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.Action).HasColumnName("action");
            entity.Property(e => e.Module).HasColumnName("module");
            entity.Property(e => e.Details).HasColumnName("details");
            entity.Property(e => e.IpAddress).HasColumnName("ip_address");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
        });
    }
}
