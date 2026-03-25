using Microsoft.EntityFrameworkCore;

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

    // TODO: Add DbSet properties for each entity table here
    // Example: public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // TODO: Configure entity relationships and constraints here
        // Example: modelBuilder.Entity<User>(entity =>
        // {
        //     entity.HasKey(e => e.Id);
        //     entity.HasIndex(e => e.Username).IsUnique();
        // });
    }
}
