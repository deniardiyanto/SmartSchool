using FluentValidation;

namespace SmartSchool.Application.Features.Authentication.Login;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username wajib diisi.")
            .MaximumLength(50);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password wajib diisi.")
            .MinimumLength(6)
            .WithMessage("Password minimal 6 karakter.");
    }
}