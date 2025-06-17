using FluentValidation;
using workstation_back_end.Experience.Domain.Models.Commands;

namespace workstation_back_end.Experience.Domain.Models.Validators;

public class CreateExperienceCommandValidator  : AbstractValidator<CreateExperienceCommand>
{
    public CreateExperienceCommandValidator()
    {
        RuleFor(e => e.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(e => e.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(e => e.Location).NotEmpty().WithMessage("Location is required.");
        RuleFor(e => e.Duration).GreaterThan(0).WithMessage("Duration must be greater than 0.");
        RuleFor(e => e.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be non-negative.");
        RuleFor(e => e.Rating).InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");
    }
}