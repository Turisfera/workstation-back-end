using workstation_back_end.Bookings.Domain;
using workstation_back_end.Bookings.Domain.Models.Entities;
using workstation_back_end.Bookings.Domain.Models.Queries;
using workstation_back_end.Bookings.Domain.Services;

namespace workstation_back_end.Bookings.Application.BookingQueryService;

public class BookingQueryService : IBookingQueryService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingQueryService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<Booking?> Handle(GetBookingByIdQuery query)
    {
        return await _bookingRepository.FindByIdAsync(query.BookingId);
    }

    public async Task<IEnumerable<Booking>> Handle(GetBookingsByTouristIdQuery query)
    {
        return await _bookingRepository.FindByTouristIdAsync(query.TouristId);
    }

    public async Task<IEnumerable<Booking>> Handle(GetBookingsByAgencyIdQuery query)
    {
        return await _bookingRepository.FindByAgencyIdAsync(query.AgencyId);
    }
}