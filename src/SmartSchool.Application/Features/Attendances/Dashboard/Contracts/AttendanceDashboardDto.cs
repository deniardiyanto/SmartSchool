namespace SmartSchool.Application.Features.Attendances.Dashboard.Contracts;

public class AttendanceDashboardDto
{
    public int TotalStudent { get; set; }

    public int Present { get; set; }

    public int Late { get; set; }

    public int NotYetScan { get; set; }

    public double AttendanceRate { get; set; }
}