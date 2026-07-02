namespace SmartSchool.Application.Features.AttendancePoints.Contracts;

public class LeaderboardResponse
{
    public List<LeaderboardDto> Items { get; set; }
        = new();
}