using tripmatch_back.Users.Domain.Models.Commands;
using tripmatch_back.Users.Domain.Models.Entities;

namespace tripmatch_back.Users.Domain.Services;


public interface IUserCommandService
{
    Task<Usuario> Handle(CreateUsuarioCommand command);
    Task<Agencia> Handle(CreateAgenciaCommand command);
    Task<Turista> Handle(CreateTuristaCommand command);
}