using SmartSchool.Domain.Entities;

namespace SmartSchool.Application.Features.AttendancePoints.Contracts;

public class AttendancePointDto
{
    public Guid Id { get; set; }

    public Guid AttendanceId { get; set; }

    public Guid StudentId { get; set; }

    public string StudentName { get; set; } = string.Empty;

    public int Point { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public static AttendancePointDto FromEntity(
        AttendancePoint entity)
    {
        return new AttendancePointDto
        {
            Id = entity.Id,
            AttendanceId = entity.AttendanceId,
            StudentId = entity.Attendance.StudentId,
            StudentName = entity.Attendance.Student.FullName,
            Point = entity.Point,
            Description = entity.Description,
            CreatedAt = entity.CreatedAt
        };
    }
}