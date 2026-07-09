namespace SmartSchool.Application.Features.Dashboard.Contracts;

public class StudentPointRankingDto
{
    public Guid StudentId { get; set; }

    public string StudentName { get; set; } = string.Empty;

    public string ClassRoom { get; set; } = string.Empty;

    public int TotalPoint { get; set; }
}