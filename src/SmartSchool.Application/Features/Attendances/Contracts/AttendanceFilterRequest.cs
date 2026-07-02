namespace SmartSchool.Application.Features.Attendances.Contracts;

public class AttendanceFilterRequest
{
    public Guid? StudentId { get; set; }

    public DateOnly? Date { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}