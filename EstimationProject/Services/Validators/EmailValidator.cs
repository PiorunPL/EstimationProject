using FluentValidation;

namespace Services.Validators;

public class EmailValidator : AbstractValidator<string>
{
    public const string EmailRegexPattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";

    public EmailValidator()
    {
        RuleFor(email => email)
            .NotNull()
            .Matches(EmailRegexPattern);
    }
}