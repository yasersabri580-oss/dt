using Doctor.Application.DTOs.Achievement;
using FluentValidation;

namespace Doctor.Application.Validators;

public class UpdateAchievementDtoValidator : AbstractValidator<UpdateAchievementDto>
{
    public UpdateAchievementDtoValidator()
    {
        RuleFor(x => x.Year)
            .InclusiveBetween(1900, DateTime.UtcNow.Year)
            .When(x => x.Year != null);
        RuleFor(x => x.IconUrl).MaximumLength(2048);
    }
}
