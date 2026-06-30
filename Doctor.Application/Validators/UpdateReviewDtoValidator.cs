using Doctor.Application.DTOs.Review;
using FluentValidation;

namespace Doctor.Application.Validators;

public class UpdateReviewDtoValidator : AbstractValidator<UpdateReviewDto>
{
    public UpdateReviewDtoValidator()
    {
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5)
            .When(x => x.Rating != null);

        RuleFor(x => x.Comment).MaximumLength(2000);
    }
}
