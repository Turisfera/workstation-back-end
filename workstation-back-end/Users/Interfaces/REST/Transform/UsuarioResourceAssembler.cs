using workstation_back_end.Users.Domain.Models.Entities;
using workstation_back_end.Users.Interfaces.REST.Resources;

namespace workstation_back_end.Users.Interfaces.REST.Transform;


public static class UsuarioResourceFromEntityAssembler
{
    public static UsuarioResource ToResource(Usuario usuario)
    {
        return new UsuarioResource
        {
            UserId = usuario.UserId,
            FirstName = usuario.FirstName,
            LastName = usuario.LastName,
            Number = usuario.Number,
            Email = usuario.Email,
            EsAgencia = usuario.Agencia != null,
            EsTurista = usuario.Turista != null
        };
    }
}