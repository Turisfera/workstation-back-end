using workstation_back_end.Users.Domain.Models.Entities;
using workstation_back_end.Users.Interfaces.REST.Resources;

namespace workstation_back_end.Users.Interfaces.REST.Transform;


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