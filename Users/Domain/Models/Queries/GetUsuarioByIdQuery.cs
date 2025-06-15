namespace tripmatch_back.Users.Domain.Models.Queries;


public record GetUsuarioByIdQuery
{
    public GetUsuarioByIdQuery(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; init; }
}