namespace SmartSchool.Application.Features.AttendancePoints.Contracts;

public class CreateAttendancePointRequest
{
    public Guid AttendanceId { get; set; }

    public int Point { get; set; }
      public string Reason { get; set; } = string.Empty;

    public string? Description { get; set; }
}