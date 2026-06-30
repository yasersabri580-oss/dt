using Doctor.Application.DTOs.SocialLink;
using FluentValidation;

namespace Doctor.Application.Validators;

public class CreateSocialLinkDtoValidator : AbstractValidator<CreateSocialLinkDto>
{
    private static readonly string[] AllowedPlatforms =
        { "Facebook", "Instagram", "Twitter", "LinkedIn", "YouTube", "TikTok", "Telegram", "WhatsApp" };

    public CreateSocialLinkDtoValidator()
    {
        RuleFor(x => x.DoctorId).NotEmpty();

        RuleFor(x => x.Platform)
            .NotEmpty()
            .Must(p => AllowedPlatforms.Contains(p, StringComparer.OrdinalIgnoreCase))
            .WithMessage($"Platform must be one of: {string.Join(", ", AllowedPlatforms)}.");

        RuleFor(x => x.Url)
            .NotEmpty().MaximumLength(2048)
            .Must(BeAValidUrl).WithMessage("Url must be a valid URL.");
    }

    private static bool BeAValidUrl(string url) =>
        Uri.TryCreate(url, UriKind.Absolute, out _);
}
