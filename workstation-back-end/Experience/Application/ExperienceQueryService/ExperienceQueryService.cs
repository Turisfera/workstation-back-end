
using workstation_back_end.Experience.Domain.Services;

namespace workstation_back_end.Experience.Domain.Models.Queries
{
    public class ExperienceQueryService(IExperienceRepository experienceRepository) : IExperienceQueryService
    {
        public async Task<IEnumerable<Domain.Models.Entities.Experience>> Handle(GetAllExperiencesQuery query)
        {
            return await experienceRepository.ListAsync();
        }
        public async Task<IEnumerable<Entities.Experience>> Handle(GetExperiencesByCategoryQuery query)
        {
            return await experienceRepository.ListByCategoryIdAsync(query.CategoryId);
        }
    }
    
}

