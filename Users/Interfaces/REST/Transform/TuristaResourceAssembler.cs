using tripmatch_back.Users.Domain.Models.Entities;
using tripmatch_back.Users.Interfaces.REST.Resources;

namespace tripmatch_back.Users.Interfaces.REST.Transform;


public static class TuristaResourceAssembler
{
    public static TuristaResource ToResource(Turista turista)
    {
        return new TuristaResource
        {
            UserId = turista.UserId,
            Edad = turista.Edad,
            Genero = turista.Genero,
            Idioma = turista.Idioma,
            Preferencias = turista.Preferencias
        };
    }
}