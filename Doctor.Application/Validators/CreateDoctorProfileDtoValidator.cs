using Doctor.Application.DTOs.DoctorProfile;
using FluentValidation;

namespace Doctor.Application.Validators;

public class CreateDoctorProfileDtoValidator : AbstractValidator<CreateDoctorProfileDto>
{
    public CreateDoctorProfileDtoValidator()
    {
        RuleFor(x => x.DoctorId).NotEmpty();

        RuleFor(x => x.Bio)
            .NotEmpty().WithMessage("At least one localized bio entry is required.");

        RuleFor(x => x.Specialization)
            .NotEmpty().WithMessage("At least one localized specialization entry is required.");

        RuleFor(x => x.AvatarUrl)
            .MaximumLength(2048)
            .Must(BeAValidUrl).WithMessage("AvatarUrl must be a valid URL.")
            .When(x => !string.IsNullOrEmpty(x.AvatarUrl));

        RuleFor(x => x.YearsOfExperience)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(80);
    }

    private static bool BeAValidUrl(string? url) =>
        Uri.TryCreate(url, UriKind.Absolute, out _);
}
