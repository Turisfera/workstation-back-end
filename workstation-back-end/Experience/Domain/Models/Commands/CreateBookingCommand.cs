namespace workstation_back_end.Experience.Domain.Models.Commands;

public record CreateBookingCommand(
    string BookingDate,
    int NumberOfPeople,
    float Price,
    string Status
);