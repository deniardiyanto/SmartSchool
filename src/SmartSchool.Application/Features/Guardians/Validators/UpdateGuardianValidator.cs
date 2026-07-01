using FluentValidation;
using SmartSchool.Application.Features.Guardians.Contracts;

namespace SmartSchool.Application.Features.Guardians.Validators;

public class UpdateGuardianValidator : AbstractValidator<UpdateGuardianRequest>
{
    public UpdateGuardianValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Email)
            .MaximumLength(100)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Address)
            .MaximumLength(255);

        RuleFor(x => x.Occupation)
            .MaximumLength(100);

        RuleFor(x => x.Relationship)
            .IsInEnum();

        RuleFor(x => x.IsActive)
            .NotNull();
    }
}