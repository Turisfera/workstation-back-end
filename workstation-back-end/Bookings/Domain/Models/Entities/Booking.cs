using System.ComponentModel.DataAnnotations.Schema;
using workstation_back_end.Shared.Domain.Model.Entities;

namespace workstation_back_end.Bookings.Domain.Models.Entities;

public class Booking : BaseEntity
{
    public DateTime BookingDate { get; set; }
    public int NumberOfPeople { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; } = string.Empty;

    // Foreign keys
    public int ExperienceId { get; set; }
    public Experience.Domain.Models.Entities.Experience Experience { get; set; } = null!;
    public Guid TouristId { get; set; }            // <--- NUEVO
    public workstation_back_end.Users.Domain.Models.Entities.Usuario Tourist { get; set; } = null!;

    public Booking() { }

    public Booking(
        Guid touristId,
        int experienceId,
        DateTime bookingDate,
        int numberOfPeople,
        decimal price,
        string status
    )
    {
        TouristId = touristId;
        ExperienceId = experienceId;
        BookingDate = bookingDate;
        NumberOfPeople = numberOfPeople;
        Price = price;
        Status = status;
    }
}