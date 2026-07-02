namespace SmartSchool.Application.Features.AttendancePoints.Contracts;

public class AttendancePointFilterRequest
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public Guid? StudentId { get; set; }

    public Guid? AttendanceId { get; set; }
}