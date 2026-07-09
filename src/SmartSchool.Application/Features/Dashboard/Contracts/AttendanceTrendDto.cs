namespace SmartSchool.Application.Features.Dashboard.Contracts;

public class AttendanceTrendDto
{
    public DateOnly Date { get; set; }

    public int Present { get; set; }

    public int Late { get; set; }

    public int Absent { get; set; }
}