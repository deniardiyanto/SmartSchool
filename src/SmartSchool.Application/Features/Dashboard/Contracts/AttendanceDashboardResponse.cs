using SmartSchool.Application.Features.Dashboard.Contracts;
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
    // ==========================
    // Sprint 7.2D-1
    // ==========================

    public int TotalStudent { get; set; }

    public decimal AttendanceRate { get; set; }
    public AttendanceStatusSummaryDto StatusSummary { get; set; }
        = new();

    public List<AttendanceTrendDto> WeeklyTrend { get; set; }
        = new();

    public List<AttendanceTrendDto> MonthlyTrend { get; set; }
        = new();
    public List<StudentPointRankingDto> TopStudents { get; set; }
        = new();

    public List<StudentPointRankingDto> BottomStudents { get; set; }
        = new();
    public WhatsAppAnalyticsDto WhatsAppAnalytics { get; set; }
        = new();

    public List<ScanPerHourDto> ScanPerHour { get; set; }
        = new();
}