namespace workstation_back_end.Reviews.Domain.Models.Commands;

/**
 * <summary>
 * Comando para crear una nueva reseña sobre una agencia.
 * </summary>
 * <param name="TouristId">ID del usuario turista que escribe la reseña.</param>
 * <param name="AgencyId">ID de la agencia que está siendo reseñada.</param>
 * <param name="Rating">Calificación en estrellas (ej. 1-5).</param>
 * <param name="Comment">Comentario de la reseña.</param>
 */
public record CreateReviewCommand(
    int TouristId,
    int AgencyId,
    int Rating,
    string Comment);