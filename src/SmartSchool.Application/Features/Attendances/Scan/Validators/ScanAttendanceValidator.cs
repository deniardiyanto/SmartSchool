using FluentValidation;
using SmartSchool.Application.Features.Attendances.Scan.Contracts;

namespace SmartSchool.Application.Features.Attendances.Scan.Validators;

public class ScanAttendanceValidator
    : AbstractValidator<ScanAttendanceRequest>
{
    public ScanAttendanceValidator()
    {
        RuleFor(x => x.BarcodeValue)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.BarcodeValue)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("Barcode wajib diisi.");
    }
}