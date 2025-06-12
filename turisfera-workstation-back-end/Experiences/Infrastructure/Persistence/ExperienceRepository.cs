using turisfera_workstation_back_end.Experiences.Domain.Models.Entities;
using turisfera_workstation_back_end.Experiences.Domain.Repositories;
using turisfera_workstation_back_end.Shared.Infrastructure.Persistence.Configuration;
using turisfera_workstation_back_end.Shared.Infrastructure.Repositories;

namespace turisfera_workstation_back_end.Experiences.Infrastructure.Persistence;

public class ExperienceRepository(AppDbContext context) : BaseRepository<Experience>(context), IExperienceRepository
{
    
}