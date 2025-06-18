namespace workstation_back_end.Booking.Domain.Models.Queries;

public record GetBookingsByExperienceQuery()
{
    public int ExperienceId { get; set; }
}