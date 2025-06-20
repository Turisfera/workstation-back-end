using Microsoft.EntityFrameworkCore;
using workstation_back_end.Bookings.Domain;
using workstation_back_end.Bookings.Domain.Models.Entities;
using workstation_back_end.Shared.Infraestructure.Persistence.Configuration;
using workstation_back_end.Shared.Infraestructure.Persistence.Repositories;
namespace workstation_back_end.Bookings.Infrastructure;

public class BookingRepository : BaseRepository<Booking>, IBookingRepository
{
    private readonly TripMatchContext _context;

    public BookingRepository(TripMatchContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Booking>> FindByTouristIdAsync(Guid touristId)
    {
        return await _context.Bookings
            .Include(b => b.Experience)
            .Where(b => b.TouristId == touristId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Booking>> FindByAgencyIdAsync(int agencyId)
    {
        return await _context.Bookings
            .Include(b => b.Experience)
            .Where(b => b.Experience.CategoryId == agencyId) 
            .ToListAsync();
    }
}