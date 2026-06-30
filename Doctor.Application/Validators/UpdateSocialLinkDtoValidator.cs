using Doctor.Application.DTOs.SocialLink;
using FluentValidation;

namespace Doctor.Application.Validators;

public class UpdateSocialLinkDtoValidator : AbstractValidator<UpdateSocialLinkDto>
{
    private static readonly string[] AllowedPlatforms =
        { "Facebook", "Instagram", "Twitter", "LinkedIn", "YouTube", "TikTok", "Telegram", "WhatsApp" };

    public UpdateSocialLinkDtoValidator()
    {
        RuleFor(x => x.Platform)
            .Must(p => AllowedPlatforms.Contains(p, StringComparer.OrdinalIgnoreCase))
            .WithMessage($"Platform must be one of: {string.Join(", ", AllowedPlatforms)}.")
            .When(x => !string.IsNullOrEmpty(x.Platform));

        RuleFor(x => x.Url)
            .MaximumLength(2048)
            .Must(BeAValidUrl).WithMessage("Url must be a valid URL.")
            .When(x => !string.IsNullOrEmpty(x.Url));
    }

    private static bool BeAValidUrl(string? url) =>
        Uri.TryCreate(url, UriKind.Absolute, out _);
}
