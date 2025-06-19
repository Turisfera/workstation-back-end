using workstation_back_end.Users.Domain.Models.Entities;
using workstation_back_end.Users.Domain.Models.Queries;

namespace workstation_back_end.Users.Domain.Services;


public interface IUserQueryService
{
    Task<Usuario?> Handle(GetUsuarioByIdQuery query);
}