using SmartSchool.Domain.Common;

namespace SmartSchool.Domain.Entities;

public class AttendancePoint : BaseAuditableEntity
{
    public Guid StudentId { get; set; }

    public Student Student { get; set; } = null!;

    public int Point { get; set; }

    public string Reason { get; set; } = string.Empty;
}