using Doctor.Application.DTOs.Article;
using FluentValidation;

namespace Doctor.Application.Validators;

public class UpdateArticleDtoValidator : AbstractValidator<UpdateArticleDto>
{
    public UpdateArticleDtoValidator()
    {
        RuleFor(x => x.Slug)
            .Must(s => s == null || s.Values.All(v => !string.IsNullOrWhiteSpace(v)))
            .WithMessage("Slug values cannot be empty.")
            .When(x => x.Slug != null);
    }
}
