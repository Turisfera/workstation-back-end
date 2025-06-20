using FluentValidation;
using workstation_back_end.Inquiry.Domain.Models.Commands;

namespace workstation_back_end.Inquiry.Domain.Services.Models.Validators;

public class CreateResponseCommandValidator : AbstractValidator<CreateResponseCommand>
{
    public CreateResponseCommandValidator()
    {
        RuleFor(r => r.InquiryId).GreaterThan(0);
        RuleFor(r => r.Answer).NotEmpty().WithMessage("Answer is required.");
    }
}