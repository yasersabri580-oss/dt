using Doctor.Application.DTOs.DoctorService;
using FluentValidation;

namespace Doctor.Application.Validators;

public class UpdateDoctorServiceDtoValidator : AbstractValidator<UpdateDoctorServiceDto>
{
    public UpdateDoctorServiceDtoValidator()
    {
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .When(x => x.Price != null);
    }
}
