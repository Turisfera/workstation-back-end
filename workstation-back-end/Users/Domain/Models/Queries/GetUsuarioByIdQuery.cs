namespace workstation_back_end.Users.Domain.Models.Queries;


public record GetUsuarioByIdQuery
{
    public GetUsuarioByIdQuery(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; init; }
}