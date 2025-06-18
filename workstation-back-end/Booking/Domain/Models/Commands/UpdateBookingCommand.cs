namespace workstation_back_end.Booking.Domain.Models.Commands;

public record UpdateBookingCommand(
    int Id,
    int ExperienceId,
    int UserId,
    string BookingDate,
    int NumberOfPeople,
    float Price,
    string Status
);