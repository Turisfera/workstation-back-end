using Microsoft.EntityFrameworkCore;
using tripmatch_back.Shared.Infrastructure.Persistence.Configuration;
using tripmatch_back.Users.Domain;
using tripmatch_back.Users.Domain.Models.Entities;

namespace tripmatch_back.Users.Infrastructure;


public class UsuarioRepository : IUsuarioRepository
{
    private readonly TripMatchContext _context;

    public UsuarioRepository(TripMatchContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Usuario entity)
    {
        await _context.Usuarios.AddAsync(entity);
    }

    public async Task<Usuario?> FindByIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<Usuario?> FindByGuidAsync(Guid userId)
    {
        return await _context.Usuarios
            .Include(u => u.Turista)
            .Include(u => u.Agencia)
            .FirstOrDefaultAsync(u => u.UserId == userId);
    }

    public void Remove(Usuario entity)
    {
        _context.Usuarios.Remove(entity);
    }

    public void Update(Usuario entity)
    {
        _context.Usuarios.Update(entity);
    }

    public async Task<IEnumerable<Usuario>> ListAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task AddAgenciaAsync(Agencia agencia)
    {
        await _context.Set<Agencia>().AddAsync(agencia);
    }

    public async Task AddTuristaAsync(Turista turista)
    {
        await _context.Set<Turista>().AddAsync(turista);
    }
}