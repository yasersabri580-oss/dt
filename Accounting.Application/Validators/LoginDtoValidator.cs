using Accounting.Application.DTOs.Auth;
using FluentValidation;

namespace Accounting.Application.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.UserType).Must(t => t == "User" || t == "Karbar")
            .WithMessage("UserType must be 'User' or 'Karbar'");
    }
}
