namespace SmartSchool.Application.Features.AttendancePoints.Contracts;

public class UpdateAttendancePointRequest
{
    public int Point { get; set; }
    public string Reason { get; set; } = string.Empty;

    public string? Description { get; set; }
}