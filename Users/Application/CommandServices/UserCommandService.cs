using FluentValidation;
using tripmatch_back.Shared.Domain;
using tripmatch_back.Users.Domain;
using tripmatch_back.Users.Domain.Models.Commands;
using tripmatch_back.Users.Domain.Models.Entities;
using tripmatch_back.Users.Domain.Models.Validadors;
using tripmatch_back.Users.Domain.Services;
using tripmatch_back.Users.Infrastructure;

namespace tripmatch_back.Users.Application.CommandServices;


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
        var result = await _validator.ValidateAsync(command);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        var usuario = new Usuario
        {
            UserId = Guid.NewGuid(),
            Nombres = command.Nombres,
            Apellidos = command.Apellidos,
            Telefono = command.Telefono,
            Email = command.Email,
            Contrasena = command.Contrasena,
            CreatedDate = DateTime.UtcNow,
            IsActive = true
        };

        await _usuarioRepository.AddAsync(usuario);
        await _unitOfWork.CompleteAsync();

        return usuario;
    }

    public async Task<Agencia> Handle(CreateAgenciaCommand command)
    {
        var agencia = new Agencia
        {
            UserId = command.UserId,
            Ruc = command.Ruc,
            Descripcion = command.Descripcion,
            LinkFacebook = command.LinkFacebook,
            LinkInstagram = command.LinkInstagram,
            CreatedDate = DateTime.UtcNow,
            IsActive = true
        };

        await _usuarioRepository.AddAgenciaAsync(agencia);
        await _unitOfWork.CompleteAsync();

        return agencia;
    }

    public async Task<Turista> Handle(CreateTuristaCommand command)
    {
        var turista = new Turista
        {
            UserId = command.UserId,
            Edad = command.Edad,
            Genero = command.Genero,
            Idioma = command.Idioma,
            Preferencias = command.Preferencias,
            CreatedDate = DateTime.UtcNow,
            IsActive = true
        };

        await _usuarioRepository.AddTuristaAsync(turista);
        await _unitOfWork.CompleteAsync();

        return turista;
    }
}