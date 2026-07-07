namespace SmartSchool.Application.Features.Dashboard.Contracts;

public class AttendanceDashboardResponse
{
    public DateOnly Date { get; set; }

    public int Present { get; set; }

    public int Late { get; set; }

    public int Absent { get; set; }

    public int CheckedOut { get; set; }

    public int TotalAttendance { get; set; }

    public int TotalPoint { get; set; }
}