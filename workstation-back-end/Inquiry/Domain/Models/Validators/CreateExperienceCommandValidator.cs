using FluentValidation;
using workstation_back_end.Inquiry.Domain.Models.Commands;

namespace workstation_back_end.Inquiry.Domain.Services.Models.Validators;

public class CreateInquiryCommandValidator : AbstractValidator<CreateInquiryCommand>
{
    public CreateInquiryCommandValidator()
    {
        RuleFor(i => i.Question).NotEmpty().WithMessage("Question is required.");
        RuleFor(i => i.ExperienceId).GreaterThan(0).WithMessage("Valid Experience ID is required.");
    }
}