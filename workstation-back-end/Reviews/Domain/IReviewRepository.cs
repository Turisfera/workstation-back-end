using workstation_back_end.Reviews.Domain.Models.Entities;
using workstation_back_end.Shared.Domain.Repositories;

namespace workstation_back_end.Reviews.Domain;

public interface IReviewRepository : IBaseRepository<Review>
{
    /**
     * <summary>
     * Encuentra todas las reseñas escritas sobre una agencia específica.
     * </summary>
     * <param name="agencyId">El ID de la agencia.</param>
     * <returns>Una colección de reseñas para la agencia.</returns>
     */
    Task<IEnumerable<Review>> FindByAgencyIdAsync(int agencyId);
}