using Doctor.Application.DTOs.ContactInfo;
using FluentValidation;

namespace Doctor.Application.Validators;

public class CreateContactInfoDtoValidator : AbstractValidator<CreateContactInfoDto>
{
    public CreateContactInfoDtoValidator()
    {
        RuleFor(x => x.DoctorId).NotEmpty();

        RuleFor(x => x.Phone)
            .NotEmpty().MaximumLength(30)
            .Matches(@"^\+?[0-9\s\-()]+$").WithMessage("Phone must be a valid phone number.")
            .When(x => !string.IsNullOrEmpty(x.Phone));

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.MapUrl)
            .MaximumLength(2048)
            .Must(BeAValidUrl).WithMessage("MapUrl must be a valid URL.")
            .When(x => !string.IsNullOrEmpty(x.MapUrl));
    }

    private static bool BeAValidUrl(string? url) =>
        Uri.TryCreate(url, UriKind.Absolute, out _);
}
