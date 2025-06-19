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
            Nombres = usuario.Nombres,
            Apellidos = usuario.Apellidos,
            Telefono = usuario.Telefono,
            Email = usuario.Email
        };
    }
}