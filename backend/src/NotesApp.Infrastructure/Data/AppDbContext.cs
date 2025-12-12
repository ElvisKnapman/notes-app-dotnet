using Microsoft.EntityFrameworkCore;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Data.Configuration;

namespace NotesApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Note> Notes { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        var now = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            // Look for an UpdatedAt property on the entity, even if the type doesn't implement any interface
            var updatedAtProp = entry.Entity.GetType().GetProperty("UpdatedAt");

            if (updatedAtProp != null && updatedAtProp.PropertyType == typeof(DateTime))
            {
                updatedAtProp.SetValue(entry.Entity, now);
            }

            // Also update CreatedAt if the entity has it and is Added
            if (entry.State == EntityState.Added)
            {
                var createdAtProp = entry.Entity.GetType().GetProperty("CreatedAt");
                if (createdAtProp != null && createdAtProp.PropertyType == typeof(DateTime))
                {
                    createdAtProp.SetValue(entry.Entity, now);
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}
