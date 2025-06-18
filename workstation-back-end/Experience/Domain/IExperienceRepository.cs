using workstation_back_end.Shared.Domain.Repositories;

namespace workstation_back_end.Experience.Domain;

public interface IExperienceRepository : IBaseRepository<Models.Entities.Experience>
{
    Task<IEnumerable<Models.Entities.Experience>> ListByCategoryIdAsync(int categoryId);
}