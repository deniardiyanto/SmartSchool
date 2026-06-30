using SmartSchool.Domain.Common;

namespace SmartSchool.Domain.Entities;

public class ClassRoom : BaseAuditableEntity
{
    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int Grade { get; set; }

    public string AcademicYear { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<Student> Students { get; set; } = new List<Student>();
}