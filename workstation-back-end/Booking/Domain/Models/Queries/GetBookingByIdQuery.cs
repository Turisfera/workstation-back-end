namespace workstation_back_end.Booking.Domain.Models.Queries;

public record GetBookingByIdQuery()
{
    public int BookingId { get; set; }
}