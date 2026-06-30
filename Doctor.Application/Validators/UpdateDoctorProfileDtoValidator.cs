using Doctor.Application.DTOs.DoctorProfile;
using FluentValidation;

namespace Doctor.Application.Validators;

public class UpdateDoctorProfileDtoValidator : AbstractValidator<UpdateDoctorProfileDto>
{
    public UpdateDoctorProfileDtoValidator()
    {
        RuleFor(x => x.AvatarUrl)
            .MaximumLength(2048)
            .Must(BeAValidUrl).WithMessage("AvatarUrl must be a valid URL.")
            .When(x => !string.IsNullOrEmpty(x.AvatarUrl));

        RuleFor(x => x.YearsOfExperience)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(80)
            .When(x => x.YearsOfExperience != null);
    }

    private static bool BeAValidUrl(string? url) =>
        Uri.TryCreate(url, UriKind.Absolute, out _);
}
