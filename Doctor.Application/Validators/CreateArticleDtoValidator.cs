using Doctor.Application.DTOs.Article;
using FluentValidation;

namespace Doctor.Application.Validators;

public class CreateArticleDtoValidator : AbstractValidator<CreateArticleDto>
{
    public CreateArticleDtoValidator()
    {
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().WithMessage("At least one localized title entry is required.");
        RuleFor(x => x.Content).NotEmpty().WithMessage("At least one localized content entry is required.");
        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("At least one localized slug entry is required.")
            .Must(s => s.Values.All(v => !string.IsNullOrWhiteSpace(v)))
            .WithMessage("Slug values cannot be empty.");
    }
}
