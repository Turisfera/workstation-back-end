using tripmatch_back.Shared.Domain;
using tripmatch_back.Shared.Infrastructure.Persistence.Configuration;

namespace tripmatch_back.Shared.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implementación de la Unidad de Trabajo. Encapsula una transacción de cambios a la base de datos.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly TripMatchContext _context;

    public UnitOfWork(TripMatchContext context)
    {
        _context = context;
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}