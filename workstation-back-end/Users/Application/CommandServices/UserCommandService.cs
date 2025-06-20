using FluentValidation;
using workstation_back_end.Shared.Domain;
using workstation_back_end.Users.Domain;
using workstation_back_end.Users.Domain.Models.Commands;
using workstation_back_end.Users.Domain.Models.Entities;
using workstation_back_end.Users.Domain.Models.Validadors;
using workstation_back_end.Users.Domain.Services;
using workstation_back_end.Users.Infrastructure;
using  workstation_back_end.Shared.Domain.Repositories;
namespace workstation_back_end.Users.Application.CommandServices;


public class UserCommandService : IUserCommandService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateUsuarioCommand> _validator;

    public UserCommandService(
        IUsuarioRepository usuarioRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateUsuarioCommand> validator)
    {
        _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<Usuario> Handle(CreateUsuarioCommand command)
    {
        var user = new Usuario
        {
            UserId = Guid.NewGuid(),
            FirstName = command.FirstName,
            LastName = command.LastName,
            Number = command.Number,
            Email = command.Email,
            Password = command.Password,
            IsActive = true,
            CreatedDate = DateTime.UtcNow
        };

        await _usuarioRepository.AddAsync(user);

        if (command.Rol == "agencia")
        {
            var agencia = new Agencia
            {
                UserId = user.UserId,
                AgencyName = command.AgencyName ?? "",
                Ruc = command.Ruc ?? "",
                Description = command.Description ?? "",
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            };

            await _usuarioRepository.AddAgenciaAsync(agencia);
        }
        else if (command.Rol == "turista")
        {
            var turista = new Turista
            {
                UserId = user.UserId,
                AvatarUrl = command.AvatarUrl ?? "",
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            };

            await _usuarioRepository.AddTuristaAsync(turista);
        }

        await _unitOfWork.CompleteAsync();
        return user;
    }
    
    public async Task UpdateAgenciaAsync(Guid userId, UpdateAgenciaCommand command)
    {
        var usuario = await _usuarioRepository.FindByGuidAsync(userId);

        if (usuario == null || usuario.Agencia == null)
            throw new Exception("No se encontró el usuario o la agencia.");

        usuario.Agencia.AgencyName = command.AgencyName;
        usuario.Agencia.Ruc = command.Ruc;
        usuario.Agencia.Description = command.Description;
        // Validar campos tipo valor (nullable)
        if (command.Rating.HasValue)
            usuario.Agencia.Rating = command.Rating.Value;

        if (command.ReviewCount.HasValue)
            usuario.Agencia.ReviewCount = command.ReviewCount.Value;

        if (command.ReservationCount.HasValue)
            usuario.Agencia.ReservationCount = command.ReservationCount.Value;
        usuario.Agencia.AvatarUrl = command.AvatarUrl;
        usuario.Agencia.ContactEmail = command.ContactEmail;
        usuario.Agencia.ContactPhone = command.ContactPhone;
        usuario.Agencia.SocialLinkFacebook = command.SocialLinkFacebook;
        usuario.Agencia.SocialLinkInstagram = command.SocialLinkInstagram;
        usuario.Agencia.SocialLinkWhatsapp = command.SocialLinkWhatsapp;

        _usuarioRepository.UpdateAgencia(usuario.Agencia);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateTuristaAsync(Guid userId, UpdateTuristaCommand command)
    {
        var usuario = await _usuarioRepository.FindByGuidAsync(userId);

        if (usuario == null || usuario.Turista == null)
            throw new Exception("No se encontró el usuario o el turista.");

        usuario.Turista.AvatarUrl = command.AvatarUrl;

        _usuarioRepository.UpdateTurista(usuario.Turista);
        await _unitOfWork.CompleteAsync();
    }
    
    public async Task DeleteUsuarioAsync(Guid userId)
    {
        var usuario = await _usuarioRepository.FindByGuidAsync(userId);
        if (usuario == null)
            throw new Exception("Usuario no encontrado");

        _usuarioRepository.Remove(usuario);
        await _unitOfWork.CompleteAsync();
    }
}