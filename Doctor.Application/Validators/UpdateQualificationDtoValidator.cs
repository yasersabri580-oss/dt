using Doctor.Application.DTOs.Qualification;
using FluentValidation;

namespace Doctor.Application.Validators;

public class UpdateQualificationDtoValidator : AbstractValidator<UpdateQualificationDto>
{
    public UpdateQualificationDtoValidator()
    {
        RuleFor(x => x.Year)
            .InclusiveBetween(1900, DateTime.UtcNow.Year)
            .When(x => x.Year != null);
    }
}
