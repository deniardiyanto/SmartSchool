using SmartSchool.Application.Features.Attendances.Dashboard.Contracts;

namespace SmartSchool.Application.Features.Attendances.Dashboard.Interfaces;

public interface IAttendanceDashboardService
{
    Task<AttendanceDashboardDto> GetTodaySummaryAsync();
}