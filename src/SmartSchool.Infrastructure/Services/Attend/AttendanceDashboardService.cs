using Microsoft.EntityFrameworkCore;
using SmartSchool.Application.Features.Attendances.Dashboard.Contracts;
using SmartSchool.Application.Features.Attendances.Dashboard.Interfaces;
using SmartSchool.Domain.Enums;
using SmartSchool.Infrastructure.Persistence.Context;

namespace SmartSchool.Infrastructure.Services.Attend;

public class AttendanceDashboardService
    : IAttendanceDashboardService
{
    private readonly SmartSchoolDbContext _context;

    public AttendanceDashboardService(
        SmartSchoolDbContext context)
    {
        _context = context;
    }

    public async Task<AttendanceDashboardDto> GetTodaySummaryAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var totalStudent = await _context.Students
            .CountAsync(x =>
                x.IsActive &&
                !x.IsDeleted);

        var present = await _context.Attendances
            .CountAsync(x =>
                x.AttendanceDate == today &&
                x.Status == AttendanceStatus.Present &&
                !x.IsDeleted);

        var late = await _context.Attendances
            .CountAsync(x =>
                x.AttendanceDate == today &&
                x.Status == AttendanceStatus.Late &&
                !x.IsDeleted);

        var scanned = await _context.Attendances
            .CountAsync(x =>
                x.AttendanceDate == today &&
                !x.IsDeleted);

        return new AttendanceDashboardDto
        {
            TotalStudent = totalStudent,

            Present = present,

            Late = late,

            NotYetScan = totalStudent - scanned,

            AttendanceRate =
                totalStudent == 0
                ? 0
                : Math.Round(
                    scanned * 100.0 / totalStudent,
                    2)
        };
    }
}