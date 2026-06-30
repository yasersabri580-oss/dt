using Doctor.Application.DTOs.HeroImage;
using FluentValidation;

namespace Doctor.Application.Validators;

public class UpdateHeroImageDtoValidator : AbstractValidator<UpdateHeroImageDto>
{
    public UpdateHeroImageDtoValidator()
    {
        RuleFor(x => x.ImageUrl)
            .MaximumLength(2048)
            .Must(BeAValidUrl).WithMessage("ImageUrl must be a valid URL.")
            .When(x => !string.IsNullOrEmpty(x.ImageUrl));

        RuleFor(x => x.SortOrder).GreaterThanOrEqualTo(0).When(x => x.SortOrder != null);
    }

    private static bool BeAValidUrl(string? url) =>
        Uri.TryCreate(url, UriKind.Absolute, out _);
}
