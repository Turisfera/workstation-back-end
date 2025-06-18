namespace workstation_back_end.Booking.Interfaces.REST.Resources;

public record BookingResource(
    int Id,
    int ExperienceId,
    string ExperienceTitle,
    int UserId,
    string UserFullName,
    string BookingDate,
    int NumberOfPeople,
    float Price,
    string Status
);