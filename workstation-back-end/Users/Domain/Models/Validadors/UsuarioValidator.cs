using FluentValidation;
using workstation_back_end.Users.Domain.Models.Commands;

namespace workstation_back_end.Users.Domain.Models.Validadors;

/// <summary>
/// Validador para la creación de un usuario base en TripMatch.
/// </summary>
public class CreateUsuarioCommandValidator : AbstractValidator<CreateUsuarioCommand>
{
    public CreateUsuarioCommandValidator()
    {
        RuleFor(u => u.Nombres)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(50).WithMessage("Máximo 50 caracteres.");

        RuleFor(u => u.Apellidos)
            .NotEmpty().WithMessage("El apellido es obligatorio.")
            .MaximumLength(50).WithMessage("Máximo 50 caracteres.");

        RuleFor(u => u.Telefono)
            .NotEmpty().WithMessage("El número de teléfono es obligatorio.")
            .GreaterThan(0).WithMessage("Debe ser un número válido.");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("El correo es obligatorio.")
            .EmailAddress().WithMessage("Formato de correo inválido.")
            .MaximumLength(30);

        RuleFor(u => u.Contrasena)
            .NotEmpty().WithMessage("La contraseña es obligatoria.")
            .MinimumLength(6).WithMessage("Mínimo 6 caracteres.")
            .MaximumLength(20).WithMessage("Máximo 20 caracteres.");
    }
}