using tripmatch_back.Users.Domain.Models.Entities;
using tripmatch_back.Users.Interfaces.REST.Resources;

namespace tripmatch_back.Users.Interfaces.REST.Transform;


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