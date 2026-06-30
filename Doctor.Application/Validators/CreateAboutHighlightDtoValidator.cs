using Doctor.Application.DTOs.AboutHighlight;
using FluentValidation;

namespace Doctor.Application.Validators;

public class CreateAboutHighlightDtoValidator : AbstractValidator<CreateAboutHighlightDto>
{
    public CreateAboutHighlightDtoValidator()
    {
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().WithMessage("At least one localized title entry is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("At least one localized description entry is required.");
        RuleFor(x => x.IconUrl).MaximumLength(2048);
        RuleFor(x => x.SortOrder).GreaterThanOrEqualTo(0);
    }
}
