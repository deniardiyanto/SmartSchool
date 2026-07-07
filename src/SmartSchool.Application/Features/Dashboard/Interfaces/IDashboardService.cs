using SmartSchool.Application.Features.Dashboard.Contracts;

namespace SmartSchool.Application.Features.Dashboard.Interfaces;

public interface IDashboardService
{
    Task<AttendanceDashboardResponse>
        GetAttendanceDashboardAsync();
}