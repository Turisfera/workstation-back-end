using Microsoft.EntityFrameworkCore;
using workstation_back_end.Shared.Infraestructure.Persistence.Configuration;
using workstation_back_end.Shared.Infraestructure.Persistence.Repositories;
using workstation_back_end.Booking.Domain.Models;

namespace workstation_back_end.Booking.Infraestructure;

public class BookingRepository : BaseRepository<Domain.Models.Entities.Booking>, IBookingRepository
{
    public BookingRepository(TripMatchContext context) : base(context) { }

    public async Task<IEnumerable<Domain.Models.Entities.Booking>> ListByBookingIdAsync(int bookingId)
    {
        return await Context.Set<Domain.Models.Entities.Booking>()
            .Include(b => b.Experience)
            .Include(b => b.User)
            .Where(b => b.Id == bookingId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Domain.Models.Entities.Booking>> ListByUserIdAsync(int userId)
    {
        return await Context.Set<Domain.Models.Entities.Booking>()
            .Include(b => b.Experience)
            .Include(b => b.User)
            .Where(b => b.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Domain.Models.Entities.Booking>> ListByExperienceIdAsync(int experienceId)
    {
        return await Context.Set<Domain.Models.Entities.Booking>()
            .Include(b => b.Experience)
            .Include(b => b.User)
            .Where(b => b.ExperienceId == experienceId)
            .ToListAsync();
    }
}