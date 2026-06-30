using Doctor.Application.DTOs.Doctor;
using FluentValidation;

namespace Doctor.Application.Validators;

public class CreateDoctorDtoValidator : AbstractValidator<CreateDoctorDto>
{
    public CreateDoctorDtoValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("At least one localized slug is required.")
            .Must(s => s.Values.All(v => !string.IsNullOrWhiteSpace(v)))
            .WithMessage("Slug values cannot be empty.");
    }
}
