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
        // Verificar si el email ya está registrado
        var existingUser = await _usuarioRepository.FindByEmailAsync(command.Email);
        if (existingUser != null)
            throw new InvalidOperationException("Este email ya está registrado.");

        var hasher = new PasswordHasher<Usuario>();
        var user = new Usuario
        {
            UserId = Guid.NewGuid(),
            Nombres = command.Nombres,
            Apellidos = command.Apellidos,
            Telefono = command.Telefono,
            Email = command.Email
        };

        user.Contrasena = hasher.HashPassword(user, command.Contrasena);
        await _usuarioRepository.AddAsync(user);
        await _unitOfWork.CompleteAsync();

        return user;
    }

    public async Task<string> SignInAsync(SignInCommand command)
    {
        var user = await _usuarioRepository.FindByEmailAsync(command.Email);
        if (user == null)
            throw new InvalidOperationException("Usuario no encontrado.");

        var hasher = new PasswordHasher<Usuario>();
        var result = hasher.VerifyHashedPassword(user, user.Contrasena, command.Contrasena);

        if (result == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException("Contraseña incorrecta.");

        return _tokenService.GenerateToken(user);
    }
}