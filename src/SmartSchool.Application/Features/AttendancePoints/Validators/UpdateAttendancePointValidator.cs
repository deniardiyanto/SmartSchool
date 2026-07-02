using FluentValidation;
using SmartSchool.Application.Features.AttendancePoints.Contracts;

namespace SmartSchool.Application.Features.AttendancePoints.Validators;

public class UpdateAttendancePointValidator
    : AbstractValidator<UpdateAttendancePointRequest>
{
    public UpdateAttendancePointValidator()
    {
        RuleFor(x => x.Point)
            .InclusiveBetween(-100, 100);

        RuleFor(x => x.Description)
            .MaximumLength(300);
    }
}