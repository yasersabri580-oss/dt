using Doctor.Application.DTOs.AboutHighlight;
using FluentValidation;

namespace Doctor.Application.Validators;

public class UpdateAboutHighlightDtoValidator : AbstractValidator<UpdateAboutHighlightDto>
{
    public UpdateAboutHighlightDtoValidator()
    {
        RuleFor(x => x.IconUrl).MaximumLength(2048);
        RuleFor(x => x.SortOrder).GreaterThanOrEqualTo(0).When(x => x.SortOrder != null);
    }
}
