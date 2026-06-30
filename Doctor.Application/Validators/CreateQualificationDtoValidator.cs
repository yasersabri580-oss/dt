using Doctor.Application.DTOs.Qualification;
using FluentValidation;

namespace Doctor.Application.Validators;

public class CreateQualificationDtoValidator : AbstractValidator<CreateQualificationDto>
{
    public CreateQualificationDtoValidator()
    {
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().WithMessage("At least one localized title entry is required.");
        RuleFor(x => x.Institution).NotEmpty().WithMessage("At least one localized institution entry is required.");
        RuleFor(x => x.Year)
            .InclusiveBetween(1900, DateTime.UtcNow.Year)
            .When(x => x.Year != null);
    }
}
