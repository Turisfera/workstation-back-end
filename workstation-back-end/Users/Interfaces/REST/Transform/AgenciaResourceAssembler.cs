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
            Ruc = agencia.Ruc,
            Descripcion = agencia.Descripcion,
            LinkFacebook = agencia.LinkFacebook,
            LinkInstagram = agencia.LinkInstagram
        };
    }
}