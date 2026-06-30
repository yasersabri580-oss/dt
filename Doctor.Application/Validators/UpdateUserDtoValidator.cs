using Doctor.Application.DTOs.User;
using FluentValidation;

namespace Doctor.Application.Validators;

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {
        RuleFor(x => x.UserName)
            .MaximumLength(100)
            .When(x => x.UserName != null);

        RuleFor(x => x.Role)
            .MaximumLength(50)
            .When(x => x.Role != null);
    }
}
