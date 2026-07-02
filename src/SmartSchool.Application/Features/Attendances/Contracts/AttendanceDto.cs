using SmartSchool.Domain.Entities;
using SmartSchool.Domain.Enums;

namespace SmartSchool.Application.Features.Attendances.Contracts;

public class AttendanceDto
{
    public Guid Id { get; set; }

    public Guid StudentId { get; set; }

    public string StudentName { get; set; } = string.Empty;

    public DateOnly AttendanceDate { get; set; }

    public DateTime? CheckInTime { get; set; }

    public DateTime? CheckOutTime { get; set; }

    public AttendanceStatus Status { get; set; }

    public string? Notes { get; set; }

    public static AttendanceDto FromEntity(
        Attendance entity)
    {
        return new AttendanceDto
        {
            Id = entity.Id,
            StudentId = entity.StudentId,
            StudentName = entity.Student.FullName,
            AttendanceDate = entity.AttendanceDate,
            CheckInTime = entity.CheckInTime,
            CheckOutTime = entity.CheckOutTime,
            Status = entity.Status,
            Notes = entity.Notes
        };
    }
}