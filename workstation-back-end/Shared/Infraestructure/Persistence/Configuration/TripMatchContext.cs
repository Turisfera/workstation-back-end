using Microsoft.EntityFrameworkCore;
using workstation_back_end.Experience.Domain.Models.Entities;
using workstation_back_end.Users.Domain.Models.Entities;

namespace workstation_back_end.Shared.Infraestructure.Persistence.Configuration
{
    public class TripMatchContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Experience.Domain.Models.Entities.Experience> Experiences { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Inquiry.Domain.Models.Entities.Inquiry> Inquiries { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
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

                entity.Property(e => e.Rating)
                    .IsRequired()
                    .HasColumnType("DECIMAL(2,1)"); 

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
            
            
            
            builder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");

                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).IsRequired();

              
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Number).IsRequired().HasMaxLength(15);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(500);


                entity.HasIndex(e => e.Email).IsUnique();

                // Relación 1:1 Usuario <-> Agencia
                entity.HasOne(u => u.Agencia)
                    .WithOne(a => a.Usuario)
                    .HasForeignKey<Agencia>(a => a.UserId);

                // Relación 1:1 Usuario <-> Turista
                entity.HasOne(u => u.Turista)
                    .WithOne(t => t.Usuario)
                    .HasForeignKey<Turista>(t => t.UserId);
            });

            // Agencia
            builder.Entity<Agencia>(entity =>
            {
                entity.ToTable("Agencias");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).IsRequired();

                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.AgencyName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Ruc).IsRequired().HasMaxLength(11);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Rating).HasColumnType("DECIMAL(2,1)");
                entity.Property(e => e.ReviewCount);
                entity.Property(e => e.ReservationCount);
                entity.Property(e => e.AvatarUrl).HasMaxLength(255);
                entity.Property(e => e.ContactEmail).HasMaxLength(100);
                entity.Property(e => e.ContactPhone).HasMaxLength(20);
                entity.Property(e => e.SocialLinkFacebook).HasMaxLength(100);
                entity.Property(e => e.SocialLinkInstagram).HasMaxLength(100);
                entity.Property(e => e.SocialLinkWhatsapp).HasMaxLength(100);
            });

            // Turista
            builder.Entity<Turista>(entity =>
            {
                entity.ToTable("Turistas");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).IsRequired();

                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.AvatarUrl).HasMaxLength(255);
            });
            //Inquiry
            builder.Entity<Inquiry.Domain.Models.Entities.Inquiry>(entity =>
            {
                entity.ToTable("Inquiries");

                entity.HasKey(i => i.Id);

                entity.Property(i => i.Question).HasMaxLength(500);
                entity.Property(i => i.Answer).HasMaxLength(500);

                entity.HasOne(i => i.Experience)
                    .WithMany() 
                    .HasForeignKey(i => i.ExperienceId);

                entity.HasOne(i => i.Usuario)
                    .WithMany()
                    .HasForeignKey(i => i.UserId);
            });
        }
        
        
       
    }
}