using tripmatch_back.Users.Domain.Models.Entities;
using tripmatch_back.Users.Domain.Models.Queries;

namespace tripmatch_back.Users.Domain.Services;


public interface IUserQueryService
{
    Task<Usuario?> Handle(GetUsuarioByIdQuery query);
}