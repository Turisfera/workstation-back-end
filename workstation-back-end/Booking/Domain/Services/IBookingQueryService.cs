using workstation_back_end.Booking.Domain.Models.Queries;

namespace workstation_back_end.Booking.Domain.Services;

public interface IBookingQueryService
{
    Task<IEnumerable<Models.Entities.Booking>> Handle(GetAllBookingsQuery query);
    Task<Models.Entities.Booking?> Handle(GetBookingByIdQuery query);
    Task<IEnumerable<Models.Entities.Booking>> Handle(GetBookingsByUserQuery query);
    Task<IEnumerable<Models.Entities.Booking>> Handle(GetBookingsByExperienceQuery query);
}