using tripmatch_back.Shared.Domain;
using tripmatch_back.Users.Domain.Models.Entities;

namespace tripmatch_back.Users.Domain;


public interface IUsuarioRepository : IBaseRepository<Usuario>
{
    Task<Usuario?> FindByGuidAsync(Guid userId);
    Task AddAgenciaAsync(Agencia agencia);
    Task AddTuristaAsync(Turista turista);
}