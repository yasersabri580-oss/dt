using Doctor.Application.DTOs.Achievement;
using FluentValidation;

namespace Doctor.Application.Validators;

public class CreateAchievementDtoValidator : AbstractValidator<CreateAchievementDto>
{
    public CreateAchievementDtoValidator()
    {
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().WithMessage("At least one localized title entry is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("At least one localized description entry is required.");
        RuleFor(x => x.Year)
            .InclusiveBetween(1900, DateTime.UtcNow.Year)
            .When(x => x.Year != null);
        RuleFor(x => x.IconUrl).MaximumLength(2048);
    }
}
