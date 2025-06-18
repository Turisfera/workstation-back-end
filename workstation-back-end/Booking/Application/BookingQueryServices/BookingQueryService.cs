using workstation_back_end.Booking.Domain.Models;
using workstation_back_end.Booking.Domain.Models.Queries;
using workstation_back_end.Booking.Domain.Services;

namespace workstation_back_end.Booking.Application.Internal.QueryServices;

public class BookingQueryService : IBookingQueryService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingQueryService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<IEnumerable<Domain.Models.Entities.Booking>> Handle(GetAllBookingsQuery query)
    {
        return await _bookingRepository.ListAsync();
    }

    public async Task<Domain.Models.Entities.Booking?> Handle(GetBookingByIdQuery query)
    {
        return await _bookingRepository.FindByIdAsync(query.BookingId);
    }

    public async Task<IEnumerable<Domain.Models.Entities.Booking>> Handle(GetBookingsByUserQuery query)
    {
        return await _bookingRepository.ListByUserIdAsync(query.UserId);
    }

    public async Task<IEnumerable<Domain.Models.Entities.Booking>> Handle(GetBookingsByExperienceQuery query)
    {
        return await _bookingRepository.ListByExperienceIdAsync(query.ExperienceId);
    }
}