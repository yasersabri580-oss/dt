using Doctor.Application.DTOs.Stat;
using FluentValidation;

namespace Doctor.Application.Validators;

public class CreateStatDtoValidator : AbstractValidator<CreateStatDto>
{
    public CreateStatDtoValidator()
    {
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.Label).NotEmpty().WithMessage("At least one localized label entry is required.");
        RuleFor(x => x.Value).NotEmpty().MaximumLength(50);
        RuleFor(x => x.IconUrl).MaximumLength(2048);
    }
}
