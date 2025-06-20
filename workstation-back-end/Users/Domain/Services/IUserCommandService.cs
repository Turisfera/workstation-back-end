using workstation_back_end.Users.Domain.Models.Commands;
using workstation_back_end.Users.Domain.Models.Entities;

namespace workstation_back_end.Users.Domain.Services;


public interface IUserCommandService
{
    Task<Usuario> Handle(CreateUsuarioCommand command);
    Task UpdateAgenciaAsync(Guid userId, UpdateAgenciaCommand command);
    Task UpdateTuristaAsync(Guid userId, UpdateTuristaCommand command);
    Task DeleteUsuarioAsync(Guid userId);

}