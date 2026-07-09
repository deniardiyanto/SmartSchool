namespace SmartSchool.Application.Features.Dashboard.Contracts;

public class AttendanceStatusSummaryDto
{
    public int Present { get; set; }

    public int Late { get; set; }

    public int Absent { get; set; }
}