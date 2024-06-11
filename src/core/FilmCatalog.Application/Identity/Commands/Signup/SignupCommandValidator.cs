using FluentValidation;

namespace FilmCatalog.Application.Identity.Commands.Signup;

public class SignupCommandValidator : AbstractValidator<SignupCommand>
{
    public SignupCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");

        RuleFor(x => x.PasswordAgain)
            .Must((x, v) => x.Password == v).WithMessage("Password must match the confirmation");
    }
}
