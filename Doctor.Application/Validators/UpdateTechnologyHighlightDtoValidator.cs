using Doctor.Application.DTOs.TechnologyHighlight;
using FluentValidation;

namespace Doctor.Application.Validators;

public class UpdateTechnologyHighlightDtoValidator : AbstractValidator<UpdateTechnologyHighlightDto>
{
    public UpdateTechnologyHighlightDtoValidator()
    {
        RuleFor(x => x.IconUrl).MaximumLength(2048);
    }
}
