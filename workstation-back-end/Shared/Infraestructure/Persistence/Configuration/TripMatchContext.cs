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
            
            
            
            builder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");

                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).IsRequired();

                entity.Property(e => e.Nombres).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Apellidos).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Telefono).IsRequired();
                entity.Property(e => e.Email).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Contrasena).IsRequired().HasMaxLength(500);

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

            builder.Entity<Agencia>(entity =>
            {
                entity.ToTable("Agencias");

                entity.HasKey(a => a.UserId);
                entity.Property(a => a.Ruc).IsRequired().HasMaxLength(11);
                entity.Property(a => a.Descripcion).IsRequired();
                entity.Property(a => a.LinkFacebook).HasMaxLength(20);
                entity.Property(a => a.LinkInstagram).HasMaxLength(20);
            });

            builder.Entity<Turista>(entity =>
            {
                entity.ToTable("Turistas");

                entity.HasKey(t => t.UserId);
                entity.Property(t => t.Edad).IsRequired();
                entity.Property(t => t.Genero).IsRequired().HasMaxLength(10);
                entity.Property(t => t.Idioma).IsRequired().HasMaxLength(20);
                entity.Property(t => t.Preferencias).IsRequired().HasMaxLength(100);
            });
        }
        
        
       
    }
}