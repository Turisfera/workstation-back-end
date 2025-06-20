using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using workstation_back_end.Experience.Domain.Models.Entities;
using workstation_back_end.Inquiry.Domain.Models.Entities;
using workstation_back_end.Users.Domain.Models.Entities;
using workstation_back_end.Bookings.Domain.Models.Entities;
using workstation_back_end.Reviews.Domain.Models.Entities;
using workstation_back_end.Shared.Domain.Model.Entities;

namespace workstation_back_end.Shared.Infraestructure.Persistence.Configuration
{
    public class TripMatchContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Experience.Domain.Models.Entities.Experience> Experiences { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Inquiry.Domain.Models.Entities.Inquiry> Inquiries { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            void ConfigureBaseEntity<TEntity>(EntityTypeBuilder<TEntity> entity) where TEntity : BaseEntity
            {
                entity.Property(e => e.CreatedDate)
                    .IsRequired()
                    .HasColumnType("datetime");
                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime");
                entity.Property(e => e.IsActive).IsRequired();
            }
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
                
                ConfigureBaseEntity(entity);
            });
            builder.Entity<Review>(entity =>
            {
                entity.ToTable("Reviews");
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Rating).IsRequired();
                entity.Property(r => r.Comment).IsRequired().HasMaxLength(1000);
                entity.Property(r => r.Date).IsRequired().HasColumnType("DATETIME");
                entity.Property(r => r.AgencyId).IsRequired();
                ConfigureBaseEntity(entity);
            });
            builder.Entity<Booking>(entity =>
            {
                entity.ToTable("Bookings"); 
                entity.HasKey(b => b.Id);   
                entity.Property(b => b.BookingDate).IsRequired().HasColumnType("DATETIME");
                entity.Property(b => b.NumberOfPeople).IsRequired();
                entity.Property(b => b.Status).IsRequired().HasMaxLength(50);
                entity.Property(b => b.Price).IsRequired(); 
                entity.HasOne(b => b.Experience) 
                    .WithMany() 
                    .HasForeignKey(b => b.ExperienceId) 
                    .IsRequired(); 
                ConfigureBaseEntity(entity);
                entity.Property(b => b.TouristId)
                    .IsRequired(); 
                entity.HasOne(b => b.Tourist)
                    .WithMany() 
                    .HasForeignKey(b => b.TouristId);
                ConfigureBaseEntity(entity);
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
                ConfigureBaseEntity(entity);
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
                entity.Property(e => e.Rating);
                entity.Property(e => e.ReviewCount);
                entity.Property(e => e.ReservationCount);
                entity.Property(e => e.AvatarUrl).HasMaxLength(255);
                entity.Property(e => e.ContactEmail).HasMaxLength(100);
                entity.Property(e => e.ContactPhone).HasMaxLength(20);
                entity.Property(e => e.SocialLinkFacebook).HasMaxLength(100);
                entity.Property(e => e.SocialLinkInstagram).HasMaxLength(100);
                entity.Property(e => e.SocialLinkWhatsapp).HasMaxLength(100);
                ConfigureBaseEntity(entity);
            });

            // Turista
            builder.Entity<Turista>(entity =>
            {
                entity.ToTable("Turistas");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).IsRequired();

                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.AvatarUrl).HasMaxLength(255);
                ConfigureBaseEntity(entity);
            });
            //Inquiry
            builder.Entity<Inquiry.Domain.Models.Entities.Inquiry>(entity =>
            {
                entity.ToTable("Inquiries");

                entity.HasKey(i => i.Id);

                entity.Property(i => i.Question).HasMaxLength(500).IsRequired();
                entity.Property(i => i.AskedAt).IsRequired().HasColumnType("datetime");

                entity.HasOne(i => i.Usuario)
                    .WithMany()
                    .HasForeignKey(i => i.UserId);

                entity.HasOne(i => i.Experience)
                    .WithMany()
                    .HasForeignKey(i => i.ExperienceId);

                entity.HasOne(i => i.Response)
                    .WithOne(r => r.Inquiry)
                    .HasForeignKey<Response>(r => r.InquiryId);
                ConfigureBaseEntity(entity);
            });

            builder.Entity<Response>(entity =>
            {
                entity.ToTable("Responses");

                entity.HasKey(r => r.Id);

                entity.Property(r => r.Answer).HasMaxLength(500).IsRequired();
                entity.Property(r => r.AnsweredAt).IsRequired().HasColumnType("datetime");

                entity.Property(r => r.ResponderId)
                    .HasColumnType("char(36)") 
                    .IsRequired();

                entity.HasOne(r => r.Responder)
                    .WithMany()
                    .HasForeignKey(r => r.ResponderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.Inquiry)
                    .WithOne(i => i.Response)
                    .HasForeignKey<Response>(r => r.InquiryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(r => r.InquiryId).IsUnique(); 
                ConfigureBaseEntity(entity);
            });
            
            builder.Entity<ExperienceImage>(entity =>
            {
                entity.ToTable("ExperienceImages");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Url).IsRequired();

                entity.HasOne(e => e.Experience)
                    .WithMany(exp => exp.ExperienceImages)
                    .HasForeignKey(e => e.ExperienceId);

                ConfigureBaseEntity(entity); 
            });

            builder.Entity<Include>(entity =>
            {
                entity.ToTable("Includes");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Description).IsRequired();

                entity.HasOne(e => e.Experience)
                    .WithMany(exp => exp.Includes)
                    .HasForeignKey(e => e.ExperienceId);

                ConfigureBaseEntity(entity); 
            });

            builder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedules");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Time).IsRequired();

                entity.HasOne(e => e.Experience)
                    .WithMany(exp => exp.Schedules)
                    .HasForeignKey(e => e.ExperienceId);

                ConfigureBaseEntity(entity); 
            });
        }
        
        
       
    }
}