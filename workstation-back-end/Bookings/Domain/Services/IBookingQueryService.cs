using workstation_back_end.Bookings.Domain.Models.Queries;

namespace workstation_back_end.Bookings.Domain.Services;

/**
 * <summary>
 * Interfaz para los servicios de consulta de Booking. Define las operaciones de solo lectura.
 * </summary>
 */
public interface IBookingQueryService
{
    /**
     * <summary>
     * Maneja la consulta para obtener una reserva por su ID.
     * </summary>
     * <param name="query">La consulta con el ID de la reserva.</param>
     * <returns>La entidad de la reserva o null si no se encuentra.</returns>
     */
    Task<Models.Entities.Booking?> Handle(GetBookingByIdQuery query);

    /**
     * <summary>
     * Maneja la consulta para obtener todas las reservas de un turista.
     * </summary>
     * <param name="query">La consulta con el ID del turista.</param>
     * <returns>Una colección de las reservas del turista.</returns>
     */
    Task<IEnumerable<Models.Entities.Booking>> Handle(GetBookingsByTouristIdQuery query);

    /**
     * <summary>
     * Maneja la consulta para obtener todas las reservas de una agencia.
     * </summary>
     * <param name="query">La consulta con el ID de la agencia.</param>
     * <returns>Una colección de las reservas de la agencia.</returns>
     */
    Task<IEnumerable<Models.Entities.Booking>> Handle(GetBookingsByAgencyIdQuery query);
}