using workstation_back_end.Shared.Domain.Model.Entities;
using workstation_back_end.Users.Domain.Models.Entities;

namespace workstation_back_end.Reviews.Domain.Models.Entities;

public class Review : BaseEntity
{
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime Date { get; set; }
    public int AgencyId { get; set; }
    public Review(int touristId, int agencyId, int rating, string comment)
    {
        UserId = touristId;
        AgencyId = agencyId;
        Rating = rating;
        Comment = comment;
        Date = DateTime.UtcNow;
    }

    public Review()
    {
        Comment = string.Empty;
    }
}