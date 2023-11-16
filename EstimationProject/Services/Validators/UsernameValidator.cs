using FluentValidation;

namespace Services.Validators;

public class UsernameValidator : AbstractValidator<string>
{
    public UsernameValidator()
    {
        RuleFor(username => username)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .MinimumLength(5);
    }
}