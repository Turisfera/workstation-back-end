using workstation_back_end.Bookings.Domain.Models.Entities;
using workstation_back_end.Bookings.Interfaces.REST.Resources;

namespace workstation_back_end.Bookings.Interfaces.REST.Transform;

/**
 * <summary>
 * Clase estática para transformar la entidad Booking a su recurso correspondiente.
 * </summary>
 */
public static class BookingAssembler
{
    public static BookingResource ToResourceFromEntity(Booking entity)
    {

        var experienceTitle = entity.Experience?.Title ?? "Título no disponible";
        
        return new BookingResource(
            entity.Id,
            entity.BookingDate,
            entity.NumberOfPeople,
            entity.Price,
            entity.Status,
            entity.ExperienceId,
            experienceTitle,
            entity.UserId 
        );
    }
}