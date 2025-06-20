namespace workstation_back_end.Bookings.Domain.Models.Queries;

/**
 * <summary>
 * Consulta para obtener todas las reservas de un turista específico.
 * </summary>
 * <param name="TouristId">ID del usuario turista.</param>
 */
public record GetBookingsByTouristIdQuery(int TouristId);