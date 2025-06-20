using FluentValidation;
using workstation_back_end.Experience.Domain.Models.Commands;

namespace workstation_back_end.Experience.Domain.Models.Validators;

public class CreateExperienceCommandValidator  : AbstractValidator<CreateExperienceCommand>
{
    public CreateExperienceCommandValidator()
    {
         RuleFor(e => e.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must be 100 characters or fewer.");

        RuleFor(e => e.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must be 500 characters or fewer.");

        RuleFor(e => e.Location)
            .NotEmpty().WithMessage("Location is required.")
            .MaximumLength(60).WithMessage("Location must be 60 characters or fewer.");

        RuleFor(e => e.Frequencies)
            .NotEmpty().WithMessage("At least one frequency is required.")
            .MaximumLength(100).WithMessage("Frequencies must be 100 characters or fewer.");

        RuleFor(e => e.Duration)
            .GreaterThan(0).WithMessage("Duration must be greater than 0.");

        RuleFor(e => e.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be non-negative.");

        RuleFor(e => e.Rating)
            .InclusiveBetween(1.0m, 5.0m) 
            .WithMessage("Rating must be between 1 and 5.");

        RuleFor(e => e.CategoryId)
            .NotEmpty().WithMessage("Category is required.");

        RuleFor(e => e.Schedules)
            .NotNull().WithMessage("At least one schedule is required.")
            .Must(s => s.Any()).WithMessage("At least one schedule is required.");

        RuleForEach(e => e.Schedules)
            .ChildRules(schedule =>
            {
                schedule.RuleFor(s => s.Time).NotEmpty().WithMessage("Schedule time is required.");
            });

        RuleFor(e => e.ExperienceImages)
            .NotNull().WithMessage("At least one image is required.")
            .Must(images => images.Count >= 1 && images.Count <= 3)
            .WithMessage("You must provide between 1 and 3 images.");

        RuleForEach(e => e.ExperienceImages)
            .ChildRules(img =>
            {
                img.RuleFor(i => i.Url).NotEmpty().WithMessage("Image URL is required.");
            });

        RuleForEach(e => e.Includes)
            .ChildRules(inc =>
            {
                inc.RuleFor(i => i.Description).NotEmpty().WithMessage("Include description is required.");
            });
    }
}