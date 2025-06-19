using workstation_back_end.Shared.Domain.Repositories;
using workstation_back_end.Users.Domain.Models.Entities;

namespace workstation_back_end.Users.Domain;


public interface IUsuarioRepository : IBaseRepository<Usuario>
{
    Task<Usuario?> FindByGuidAsync(Guid userId);
    Task AddAgenciaAsync(Agencia agencia);
    Task AddTuristaAsync(Turista turista);
    Task<Usuario?> FindByEmailAsync(string email);
}