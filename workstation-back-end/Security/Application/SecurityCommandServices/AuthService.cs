using Microsoft.AspNetCore.Identity;
using workstation_back_end.Security.Domain.Models.Commands;
using workstation_back_end.Security.Domain.Services;
using workstation_back_end.Users.Domain;
using workstation_back_end.Users.Domain.Models.Entities;
using workstation_back_end.Shared.Domain.Repositories;
using workstation_back_end.Users.Infrastructure;


namespace workstation_back_end.Security.Application.SecurityCommandServices;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(IUsuarioRepository usuarioRepository, ITokenService tokenService, IUnitOfWork unitOfWork)
    {
        _usuarioRepository = usuarioRepository;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Usuario> SignUpAsync(SignUpCommand command)
    {
        // Verificar duplicado por email
        var existingUser = await _usuarioRepository.FindByEmailAsync(command.Email);
        if (existingUser != null)
            throw new InvalidOperationException("Este correo electrónico ya está registrado.");

        var hasher = new PasswordHasher<Usuario>();

        // Crear usuario base
        var user = new Usuario
        {
            UserId = Guid.NewGuid(),
            FirstName = command.FirstName,
            LastName = command.LastName,
            Number = command.Number,
            Email = command.Email,
            CreatedDate = DateTime.UtcNow,
            IsActive = true
        };

        user.Password = hasher.HashPassword(user, command.Password);
        await _usuarioRepository.AddAsync(user);

        // Crear perfil según rol
        if (command.Rol?.ToLower() == "agencia")
        {
            var agencia = new Agencia
            {
                UserId = user.UserId,
                AgencyName = command.AgencyName ?? "",
                Ruc = command.Ruc ?? "",
                Description = "",
                Rating = 0,
                ReviewCount = 0,
                ReservationCount = 0,
                AvatarUrl = "",
                ContactEmail = "",
                ContactPhone = "",
                SocialLinkFacebook = "",
                SocialLinkInstagram = "",
                SocialLinkWhatsapp = "",
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            };

            await _usuarioRepository.AddAgenciaAsync(agencia);
        }
        else if (command.Rol?.ToLower() == "turista")
        {
            var turista = new Turista
            {
                UserId = user.UserId,
                AvatarUrl = "",
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            };

            await _usuarioRepository.AddTuristaAsync(turista);
        }

        await _unitOfWork.CompleteAsync();
        return user;
    }

    public async Task<string> SignInAsync(SignInCommand command)
    {
        var user = await _usuarioRepository.FindByEmailAsync(command.Email);
        if (user == null)
            throw new InvalidOperationException("Usuario no encontrado.");

        var hasher = new PasswordHasher<Usuario>();
        var result = hasher.VerifyHashedPassword(user, user.Password, command.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException("Contraseña incorrecta.");

        return _tokenService.GenerateToken(user);
    }
}