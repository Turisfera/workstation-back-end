using Microsoft.EntityFrameworkCore;
using workstation_back_end.Experience.Domain;
using workstation_back_end.Experience.Domain.Models.Entities;
using workstation_back_end.Shared.Infraestructure.Persistence.Configuration;
using workstation_back_end.Shared.Infraestructure.Persistence.Repositories;

public class ExperienceRepository : BaseRepository<Experience>, IExperienceRepository
{
    private readonly TripMatchContext _context;

    public ExperienceRepository(TripMatchContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Experience>> ListAsync()
    {
        return await _context.Experiences
            .Include(e => e.ExperienceImages)
            .Include(e => e.Includes)
            .Include(e => e.Schedules)
            .ToListAsync();
    }
    

    public async Task<IEnumerable<Experience>> ListByCategoryIdAsync(int categoryId)
    {
        return await _context.Set<Experience>()
            .Where(e => e.CategoryId == categoryId && e.IsActive)
            .Include(e => e.ExperienceImages)
            .Include(e => e.Includes)
            .Include(e => e.Schedules)
            .ToListAsync();
    }
}