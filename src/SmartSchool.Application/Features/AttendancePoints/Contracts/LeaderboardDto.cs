namespace SmartSchool.Application.Features.AttendancePoints.Contracts;

public class LeaderboardDto
{
    public Guid StudentId { get; set; }

    public string NIS { get; set; } = string.Empty;

    public string StudentName { get; set; } = string.Empty;

    public string ClassRoomName { get; set; } = string.Empty;

    public int TotalPoint { get; set; }

    public int Rank { get; set; }
}