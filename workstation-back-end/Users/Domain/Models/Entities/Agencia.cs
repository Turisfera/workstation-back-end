using workstation_back_end.Shared.Domain.Model.Entities;

namespace workstation_back_end.Users.Domain.Models.Entities;


/// <summary>
/// Representa a un usuario con perfil de agencia.
/// </summary>
public class Agencia : BaseEntity
{
    public Guid UserId { get; set; }

    public string AgencyName { get; set; }

    public string Ruc { get; set; }

    public string Description { get; set; }

    public float Rating { get; set; }

    public int ReviewCount { get; set; }

    public int ReservationCount { get; set; }

    public string? AvatarUrl { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public string? SocialLinkFacebook { get; set; }

    public string? SocialLinkInstagram { get; set; }

    public string? SocialLinkWhatsapp { get; set; }

    public Usuario? Usuario { get; set; }
}