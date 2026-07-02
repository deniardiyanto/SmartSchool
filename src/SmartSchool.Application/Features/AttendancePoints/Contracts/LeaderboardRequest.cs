namespace SmartSchool.Application.Features.AttendancePoints.Contracts;

public class LeaderboardRequest
{
    public Guid? ClassRoomId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int Top { get; set; } = 20;
}