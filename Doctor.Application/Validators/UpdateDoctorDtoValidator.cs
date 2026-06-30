using Doctor.Application.DTOs.Doctor;
using FluentValidation;

namespace Doctor.Application.Validators;

public class UpdateDoctorDtoValidator : AbstractValidator<UpdateDoctorDto>
{
    public UpdateDoctorDtoValidator()
    {
        RuleFor(x => x.Slug)
            .Must(s => s == null || s.Values.All(v => !string.IsNullOrWhiteSpace(v)))
            .WithMessage("Slug values cannot be empty.")
            .When(x => x.Slug != null);
    }
}
