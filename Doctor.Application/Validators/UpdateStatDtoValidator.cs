using Doctor.Application.DTOs.Stat;
using FluentValidation;

namespace Doctor.Application.Validators;

public class UpdateStatDtoValidator : AbstractValidator<UpdateStatDto>
{
    public UpdateStatDtoValidator()
    {
        RuleFor(x => x.Value).MaximumLength(50).When(x => x.Value != null);
        RuleFor(x => x.IconUrl).MaximumLength(2048);
    }
}
