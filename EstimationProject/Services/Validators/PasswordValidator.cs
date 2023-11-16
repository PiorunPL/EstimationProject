using FluentValidation;

namespace Services.Validators;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(password => password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(50);
    }
}