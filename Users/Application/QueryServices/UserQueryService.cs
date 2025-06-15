using tripmatch_back.Users.Domain;
using tripmatch_back.Users.Domain.Models.Entities;
using tripmatch_back.Users.Domain.Models.Queries;
using tripmatch_back.Users.Domain.Services;

namespace tripmatch_back.Users.Application.QueryServices;


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