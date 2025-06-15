using Microsoft.EntityFrameworkCore;
using tripmatch_back.Users.Domain.Models.Entities;

namespace tripmatch_back.Shared.Infrastructure.Persistence.Configuration;


public class TripMatchContext : DbContext
{
    public TripMatchContext(DbContextOptions options) : base(options) { }

    // DbSet de la entidad Usuario
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        // Aquí puedes configurar logging, conexión directa, etc. si lo deseas
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuarios");

            entity.HasKey(e => e.UserId);
            entity.Property(e => e.UserId).IsRequired();

            entity.Property(e => e.Nombres).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Apellidos).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Telefono).IsRequired();
            entity.Property(e => e.Email).IsRequired().HasMaxLength(30);
            entity.Property(e => e.Contrasena).IsRequired().HasMaxLength(20);

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