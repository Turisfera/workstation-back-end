using workstation_back_end.Shared.Domain.Model.Entities;
using workstation_back_end.Users.Domain.Models.Entities;

namespace workstation_back_end.Reviews.Domain.Models.Entities;

public class Review : BaseEntity
{
    public Guid TouristId { get; set; }      
    public int AgencyId { get; set; }     
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime Date { get; set; }

    public Review() { }

    public Review(
        Guid touristId, 
        int agencyId, 
        int rating, 
        string comment)
    {
        TouristId = touristId;
        AgencyId = agencyId;
        Rating = rating;
        Comment = comment;
        Date = DateTime.UtcNow;
    }
}