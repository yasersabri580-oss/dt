using Doctor.Application.DTOs.AppointmentOption;
using FluentValidation;

namespace Doctor.Application.Validators;

public class UpdateAppointmentOptionDtoValidator : AbstractValidator<UpdateAppointmentOptionDto>
{
    public UpdateAppointmentOptionDtoValidator()
    {
        RuleFor(x => x.DurationMinutes)
            .GreaterThan(0).LessThanOrEqualTo(480)
            .When(x => x.DurationMinutes != null);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .When(x => x.Price != null);
    }
}
