using tripmatch_back.Users.Domain.Models.Entities;
using tripmatch_back.Users.Interfaces.REST.Resources;

namespace tripmatch_back.Users.Interfaces.REST.Transform;


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