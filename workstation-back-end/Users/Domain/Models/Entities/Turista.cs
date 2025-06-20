using workstation_back_end.Shared.Domain.Model.Entities;

namespace workstation_back_end.Users.Domain.Models.Entities;


/// <summary>
/// Representa a un usuario con perfil de turista.
/// </summary>
public class Turista : BaseEntity
{
    public Guid UserId { get; set; }

    public string? AvatarUrl { get; set; }

    public Usuario? Usuario { get; set; }
}