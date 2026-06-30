using Doctor.Application.DTOs.AppointmentOption;
using FluentValidation;

namespace Doctor.Application.Validators;

public class CreateAppointmentOptionDtoValidator : AbstractValidator<CreateAppointmentOptionDto>
{
    public CreateAppointmentOptionDtoValidator()
    {
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().WithMessage("At least one localized title entry is required.");
        RuleFor(x => x.DurationMinutes).GreaterThan(0).LessThanOrEqualTo(480);
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
    }
}
