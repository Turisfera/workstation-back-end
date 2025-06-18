using Microsoft.EntityFrameworkCore;
using workstation_back_end.Reviews.Domain;
using workstation_back_end.Reviews.Domain.Models.Entities;
using workstation_back_end.Shared.Infraestructure.Persistence.Configuration;
using workstation_back_end.Shared.Infraestructure.Persistence.Repositories;

namespace workstation_back_end.Reviews.Infrastructure;

public class ReviewRepository : BaseRepository<Review>, IReviewRepository
{
    private readonly TripMatchContext _context;

    public ReviewRepository(TripMatchContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Review>> FindByAgencyIdAsync(int agencyId)
    {
        return await _context.Reviews
            .Where(r => r.AgencyId == agencyId)
            // .Include(r => r.User) 
            .ToListAsync();
    }
}