using workstation_back_end.Users.Domain.Models.Commands;
using workstation_back_end.Users.Domain.Models.Entities;

namespace workstation_back_end.Users.Domain.Services;


public interface IUserCommandService
{
    Task<Usuario> Handle(CreateUsuarioCommand command);
    Task<Agencia> Handle(CreateAgenciaCommand command);
    Task<Turista> Handle(CreateTuristaCommand command);
}