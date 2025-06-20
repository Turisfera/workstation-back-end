using Microsoft.EntityFrameworkCore;
using workstation_back_end.Shared.Infraestructure.Persistence.Configuration;
using workstation_back_end.Users.Domain;
using workstation_back_end.Users.Domain.Models.Entities;

namespace workstation_back_end.Users.Infrastructure;


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
    public async Task<Usuario?> FindByEmailAsync(string email)
    {
        return await _context.Usuarios
            .Include(u => u.Turista)
            .Include(u => u.Agencia)
            .FirstOrDefaultAsync(u => u.Email == email);
    }
    public void UpdateAgencia(Agencia agencia)
    {
        _context.Set<Agencia>().Update(agencia);
    }

    public void UpdateTurista(Turista turista)
    {
        _context.Set<Turista>().Update(turista);
    }
    
}