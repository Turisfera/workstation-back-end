using workstation_back_end.Shared.Domain.Model.Entities;

namespace workstation_back_end.Users.Domain.Models.Entities;

/// <summary>
/// Representa a un usuario base en TripMatch. Puede ser una agencia o un turista.
/// </summary>
public class Usuario : BaseEntity
{
    public Guid UserId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Number { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public Turista? Turista { get; set; }

    public Agencia? Agencia { get; set; }
}