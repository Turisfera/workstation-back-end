using Microsoft.EntityFrameworkCore;
using turisfera_workstation_back_end.Experiences.Domain.Models.Entities; // Importante

namespace turisfera_workstation_back_end.Shared.Infrastructure.Persistence.Configuration;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Experience>().ToTable("Experiences");
        builder.Entity<Experience>().HasKey(p => p.Id);
        builder.Entity<Experience>().Property(p => p.Title).IsRequired().HasMaxLength(100);
        builder.Entity<Experience>().Property(p => p.Description).HasMaxLength(500);
        builder.Entity<Experience>().Property(p => p.Price).IsRequired();
        builder.Entity<Category>().ToTable("Categories");
        builder.Entity<Category>().HasKey(p => p.Id);
        builder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Category>()
            .HasMany(c => c.Experiences)
            .WithOne(e => e.Category)
            .HasForeignKey(e => e.CategoryId);
    }
}