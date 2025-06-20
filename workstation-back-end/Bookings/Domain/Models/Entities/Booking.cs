using System.ComponentModel.DataAnnotations.Schema;
using workstation_back_end.Shared.Domain.Model.Entities;

namespace workstation_back_end.Bookings.Domain.Models.Entities;

public class Booking : BaseEntity
{
 
    public DateTime BookingDate { get; set; }
    public int NumberOfPeople { get; set; }
    
    [Column(TypeName = "DECIMAL(10,2)")] 
    public decimal Price { get; set; }
    
    public string Status { get; set; } 


    public int ExperienceId { get; set; }
    public Experience.Domain.Models.Entities.Experience Experience { get; set; }
    
    public Booking()
    {
        Status = string.Empty;
        Experience = null!;
    }
    
    public Booking(int userId, int experienceId, DateTime bookingDate, int numberOfPeople, decimal price, string status)
    {
        UserId = userId; 
        ExperienceId = experienceId;
        BookingDate = bookingDate;
        NumberOfPeople = numberOfPeople;
        Price = price;
        Status = status;
    }
}