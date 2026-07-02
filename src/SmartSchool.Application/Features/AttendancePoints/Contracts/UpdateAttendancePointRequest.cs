namespace SmartSchool.Application.Features.AttendancePoints.Contracts;

public class UpdateAttendancePointRequest
{
    public int Point { get; set; }

    public string? Description { get; set; }
}