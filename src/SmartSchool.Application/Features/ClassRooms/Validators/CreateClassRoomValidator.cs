using FluentValidation;
using SmartSchool.Application.Features.ClassRooms.Contracts;

namespace SmartSchool.Application.Features.ClassRooms.Validators;

public class CreateClassRoomValidator : AbstractValidator<CreateClassRoomRequest>
{
    public CreateClassRoomValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Grade)
            .InclusiveBetween(1, 12);

        RuleFor(x => x.AcademicYear)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Description)
            .MaximumLength(255);
    }
}