using workstation_back_end.Booking.Interfaces.REST.Resources;

namespace workstation_back_end.Booking.Interfaces.REST.Transform;

/// <summary>
/// Assembler to convert Booking entities to BookingResource
/// </summary>
public static class BookingResourceFromEntityAssembler
{
    /// <summary>
    /// Converts a Booking entity to a BookingResource
    /// </summary>
    /// <param name="booking">The booking entity</param>
    /// <returns>BookingResource</returns>
    public static BookingResource ToResourceFromEntity(Domain.Models.Entities.Booking booking)
    {
        return new BookingResource(
            booking.Id,
            booking.ExperienceId,
            booking.Experience?.Title ?? "Unknown Experience",
            booking.UserId,
            booking.User != null ? $"{booking.User.FirstName} {booking.User.LastName}" : "Unknown User",
            booking.BookingDate,
            booking.NumberOfPeople,
            booking.Price,
            booking.Status
        );
    }
}