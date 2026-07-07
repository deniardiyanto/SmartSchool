using Microsoft.EntityFrameworkCore;
using SmartSchool.Application.Common.Interfaces;
using SmartSchool.Application.Features.Dashboard.Contracts;
using SmartSchool.Application.Features.Dashboard.Interfaces;
using SmartSchool.Domain.Enums;
using SmartSchool.Infrastructure.Persistence.Context;

namespace SmartSchool.Infrastructure.Services.Dashboard;

public class DashboardService : IDashboardService
{
    private readonly SmartSchoolDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public DashboardService(
        SmartSchoolDbContext context,
        IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<AttendanceDashboardResponse>
        GetAttendanceDashboardAsync()
    {
        var today = DateOnly.FromDateTime(
            _dateTimeProvider.UtcNow);

        //----------------------------------------------------
        // Attendance Today
        //----------------------------------------------------

        var attendances = _context.Attendances
            .Where(x =>
                !x.IsDeleted &&
                x.AttendanceDate == today);

        //----------------------------------------------------
        // Present
        //----------------------------------------------------

        var present = await attendances
            .CountAsync(x =>
                x.Status == AttendanceStatus.Present);

        //----------------------------------------------------
        // Late
        //----------------------------------------------------

        var late = await attendances
            .CountAsync(x =>
                x.Status == AttendanceStatus.Late);

        //----------------------------------------------------
        // Absent
        //----------------------------------------------------

        var absent = await attendances
            .CountAsync(x =>
                x.Status == AttendanceStatus.Absent);

        //----------------------------------------------------
        // Checked Out
        //----------------------------------------------------

        var checkedOut = await attendances
            .CountAsync(x =>
                x.CheckOutTime != null);

        //----------------------------------------------------
        // Total Attendance
        //----------------------------------------------------

        var totalAttendance = await attendances
            .CountAsync();

        //----------------------------------------------------
        // Total Point
        //----------------------------------------------------

        var totalPoint = await _context.AttendancePoints
            .Where(x =>
                !x.IsDeleted &&
                x.PointDate.Date ==
                _dateTimeProvider.UtcNow.Date)
            .SumAsync(x => (int?)x.Point) ?? 0;

        //----------------------------------------------------
        // Response
        //----------------------------------------------------

        return new AttendanceDashboardResponse
        {
            Date = today,

            Present = present,

            Late = late,

            Absent = absent,

            CheckedOut = checkedOut,

            TotalAttendance = totalAttendance,

            TotalPoint = totalPoint
        };
    }
}