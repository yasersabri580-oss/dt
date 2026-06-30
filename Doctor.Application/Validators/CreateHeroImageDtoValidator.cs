using Doctor.Application.DTOs.HeroImage;
using FluentValidation;

namespace Doctor.Application.Validators;

public class CreateHeroImageDtoValidator : AbstractValidator<CreateHeroImageDto>
{
    public CreateHeroImageDtoValidator()
    {
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.ImageUrl)
            .NotEmpty().MaximumLength(2048)
            .Must(BeAValidUrl).WithMessage("ImageUrl must be a valid URL.");
        RuleFor(x => x.SortOrder).GreaterThanOrEqualTo(0);
    }

    private static bool BeAValidUrl(string url) =>
        Uri.TryCreate(url, UriKind.Absolute, out _);
}
