using Doctor.Application.DTOs.DoctorService;
using FluentValidation;

namespace Doctor.Application.Validators;

public class CreateDoctorServiceDtoValidator : AbstractValidator<CreateDoctorServiceDto>
{
    public CreateDoctorServiceDtoValidator()
    {
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().WithMessage("At least one localized title entry is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("At least one localized description entry is required.");
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
    }
}
