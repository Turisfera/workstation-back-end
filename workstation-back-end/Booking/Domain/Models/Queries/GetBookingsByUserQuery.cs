namespace workstation_back_end.Booking.Domain.Models.Queries;

public record GetBookingsByUserQuery()
{
    public int UserId { get; set; }
}