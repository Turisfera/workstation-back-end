using workstation_back_end.Shared.Domain.Model.Entities;

namespace workstation_back_end.Booking.Domain.Models.Entities
{
    public class Booking : BaseEntity
    {
        public Booking(int experienceId, int userId, string bookingDate, int numberOfPeople, float price, string status)
        {
            ExperienceId = experienceId;
            UserId = userId;
            BookingDate = bookingDate;
            NumberOfPeople = numberOfPeople;
            Price = price;
            Status = status;
        }

        public int ExperienceId { get; set; }
        public int UserId { get; set; }
        public Experience.Domain.Models.Entities.Experience Experience { get; set; }
        public User User { get; set; }
        public string BookingDate { get; set; }
        public int NumberOfPeople { get; set; }
        public float Price { get; set; }
        public string Status { get; set; }
    }
}