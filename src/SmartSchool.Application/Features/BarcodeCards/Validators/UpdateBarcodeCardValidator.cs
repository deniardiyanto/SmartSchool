using FluentValidation;
using SmartSchool.Application.Features.BarcodeCards.Contracts;

namespace SmartSchool.Application.Features.BarcodeCards.Validators;

public class UpdateBarcodeCardValidator
    : AbstractValidator<UpdateBarcodeCardRequest>
{
    public UpdateBarcodeCardValidator()
    {
        RuleFor(x => x.CardNumber)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.BarcodeValue)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.IssuedDate)
            .NotEmpty();
    }
}