using workstation_back_end.Users.Domain.Models.Entities;
using workstation_back_end.Users.Interfaces.REST.Resources;

namespace workstation_back_end.Users.Interfaces.REST.Transform;


public static class AgenciaResourceAssembler
{
    public static AgenciaResource ToResource(Agencia agencia)
    {
        return new AgenciaResource
        {
            UserId = agencia.UserId,
            AgencyName = agencia.AgencyName,
            Ruc = agencia.Ruc,
            Description = agencia.Description,
            Rating = agencia.Rating,
            ReviewCount = agencia.ReviewCount,
            ReservationCount = agencia.ReservationCount,
            AvatarUrl = agencia.AvatarUrl,
            ContactEmail = agencia.ContactEmail,
            ContactPhone = agencia.ContactPhone,
            SocialLinkFacebook = agencia.SocialLinkFacebook,
            SocialLinkInstagram = agencia.SocialLinkInstagram,
            SocialLinkWhatsapp = agencia.SocialLinkWhatsapp
        };
    }
}