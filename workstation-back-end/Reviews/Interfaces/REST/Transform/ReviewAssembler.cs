using workstation_back_end.Reviews.Domain.Models.Entities;
using workstation_back_end.Reviews.Interfaces.REST.Resources;

namespace workstation_back_end.Reviews.Interfaces.REST.Transform;

public static class ReviewAssembler
{
    public static ReviewResource ToResourceFromEntity(Review entity)
    {
        var touristName = "Usuario Anónimo"; 
        return new ReviewResource(
            entity.Id,
            entity.Rating,
            entity.Comment,
            entity.Date,
            entity.TouristId, 
            touristName,
            null,
            entity.AgencyId
        );
    }
}