using Doctor.Application.DTOs.Faq;
using FluentValidation;

namespace Doctor.Application.Validators;

public class CreateFaqDtoValidator : AbstractValidator<CreateFaqDto>
{
    public CreateFaqDtoValidator()
    {
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.Question).NotEmpty().WithMessage("At least one localized question entry is required.");
        RuleFor(x => x.Answer).NotEmpty().WithMessage("At least one localized answer entry is required.");
        RuleFor(x => x.SortOrder).GreaterThanOrEqualTo(0);
    }
}
