using Doctor.Application.DTOs.Faq;
using FluentValidation;

namespace Doctor.Application.Validators;

public class UpdateFaqDtoValidator : AbstractValidator<UpdateFaqDto>
{
    public UpdateFaqDtoValidator()
    {
        RuleFor(x => x.SortOrder).GreaterThanOrEqualTo(0).When(x => x.SortOrder != null);
    }
}
