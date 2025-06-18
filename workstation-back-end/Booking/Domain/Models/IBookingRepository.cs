using workstation_back_end.Shared.Domain.Repositories;

namespace workstation_back_end.Booking.Domain.Models;

public interface IBookingRepository : IBaseRepository<Models.Entities.Booking>
{
    Task<IEnumerable<Models.Entities.Booking>> ListByBookingIdAsync(int bookingId);
    Task<IEnumerable<Models.Entities.Booking>> ListByUserIdAsync(int userId);
    Task<IEnumerable<Models.Entities.Booking>> ListByExperienceIdAsync(int experienceId);
}