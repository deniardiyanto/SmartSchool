using SmartSchool.Domain.Common;
using SmartSchool.Domain.Enums;

namespace SmartSchool.Domain.Entities;

public class Attendance : BaseAuditableEntity
{
    public Guid StudentId { get; set; }

    public Student Student { get; set; } = null!;

    public DateTime ScanTime { get; set; }

    public AttendanceStatus Status { get; set; }

    public int Point { get; set; }

    public Guid ScanBy { get; set; }

    public User Scanner { get; set; } = null!;

    public string? Notes { get; set; }
}