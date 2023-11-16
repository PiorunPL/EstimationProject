using FluentValidation;
using Services.Validators;
using WebCommunication.Contracts.UserContracts;

namespace Services.Services.UserService.Validators;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(request => request.Email)
            .SetValidator(new EmailValidator());
        RuleFor(request => request.Username)
            .SetValidator(new UsernameValidator());
        RuleFor(request => request.Password)
            .SetValidator(new PasswordValidator());
    }
}