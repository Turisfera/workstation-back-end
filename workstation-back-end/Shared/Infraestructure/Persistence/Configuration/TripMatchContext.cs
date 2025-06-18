using Microsoft.EntityFrameworkCore;
using workstation_back_end.Experience.Domain.Models.Entities;

namespace workstation_back_end.Shared.Infraestructure.Persistence.Configuration
{
    public class TripMatchContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Experience.Domain.Models.Entities.Experience> Experiences { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Category> Categories { get; set; }
        
        public DbSet<Booking.Domain.Models.Entities.User> Users { get; set; }
        public DbSet<Booking.Domain.Models.Entities.Booking> Bookings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Experience Entity Configuration
            builder.Entity<Experience.Domain.Models.Entities.Experience>(entity =>
            {
                entity.ToTable("Experiences");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasAnnotation("CheckConstraint", "LEN(Title) > 0");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasAnnotation("CheckConstraint", "LEN(Description) > 0");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Frequencies)
                    .HasMaxLength(100);

                entity.Property(e => e.Duration)
                    .IsRequired();

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnType("DECIMAL(10,2)");

                entity.Property(e => e.Rating)
                    .IsRequired();

                entity.HasIndex(e => e.Title)
                      .IsUnique();
            });
            
            builder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(c => c.Name).IsUnique();
            });

            builder.Entity<Experience.Domain.Models.Entities.Experience>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Experiences)
                .HasForeignKey(e => e.CategoryId);

            builder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.UserId);

                entity.Property(u => u.FirstName)
                    .IsRequired()
                    .HasMaxLength(20);
                
                entity.Property(u => u.LastName)
                    .IsRequired()
                    .HasMaxLength(20);
                
                entity.Property(u => u.Number)
                    .IsRequired();
                
                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(30);
                
                entity.Property(u => u.Password)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            builder.Entity<Booking.Domain.Models.Entities.Booking>(entity =>
            {
                entity.ToTable("Bookings");
                entity.HasKey(b => b.Id);

                entity.Property(b => b.BookingDate)
                    .IsRequired();
                
                entity.Property(b => b.NumberOfPeople)
                    .IsRequired();
                
                entity.Property(b => b.Price)
                    .IsRequired();

                entity.Property(b => b.Status)
                    .IsRequired();

                entity.HasOne(b => b.Experience)
                    .WithMany()
                    .HasForeignKey(b => b.ExperienceId);
                
                entity.HasOne(b => b.User)
                    .WithMany()
                    .HasForeignKey(b => b.UserId);
            });
        }
    }
}