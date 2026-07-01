using FluentValidation;
using SmartSchool.Application.Features.Guardians.Contracts;

namespace SmartSchool.Application.Features.Guardians.Validators;

public class CreateGuardianValidator
    : AbstractValidator<CreateGuardianRequest>
{
    public CreateGuardianValidator()
    {
        RuleFor(x => x.GuardianCode)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Gender)
            .NotEmpty()
            .Must(x => x == "Male" || x == "Female")
            .WithMessage("Gender must be either 'Male' or 'Female'.");
    }
}