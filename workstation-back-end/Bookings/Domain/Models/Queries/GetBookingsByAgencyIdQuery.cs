namespace workstation_back_end.Bookings.Domain.Models.Queries;

/**
 * <summary>
 * Consulta para obtener todas las reservas de las experiencias que pertenecen a una agencia.
 * </summary>
 * <param name="AgencyId">ID del usuario de la agencia.</param>
 */
public record GetBookingsByAgencyIdQuery(int AgencyId);