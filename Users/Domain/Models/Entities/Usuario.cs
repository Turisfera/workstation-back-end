using tripmatch_back.Shared.Domain.Model.Entities;

namespace tripmatch_back.Users.Domain.Models.Entities;

/// <summary>
/// Representa a un usuario base en TripMatch. Puede ser una agencia o un turista.
/// </summary>
public class Usuario : BaseEntity
{
    public Guid UserId { get; set; }           // UUID como clave primaria
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public int Telefono { get; set; }
    public string Email { get; set; }   
    public string Contrasena { get; set; }

    // Relaciones (opcionalmente puedes añadir navegación más adelante)
    public Agencia? Agencia { get; set; }
    public Turista? Turista { get; set; }
}