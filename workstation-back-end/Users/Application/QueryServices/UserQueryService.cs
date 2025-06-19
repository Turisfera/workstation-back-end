using workstation_back_end.Users.Domain;
using workstation_back_end.Users.Domain.Models.Entities;
using workstation_back_end.Users.Domain.Models.Queries;
using workstation_back_end.Users.Domain.Services;

namespace workstation_back_end.Users.Application.QueryServices;


public class UserQueryService : IUserQueryService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UserQueryService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
    }

    public async Task<Usuario?> Handle(GetUsuarioByIdQuery query)
    {
        return await _usuarioRepository.FindByGuidAsync(query.UserId);
    }
}   