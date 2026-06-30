using SmartSchool.Domain.Common;

namespace SmartSchool.Domain.Entities;

public class ClassRoom : BaseSoftDeleteEntity
{
    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public ICollection<Student> Students { get; set; }
        = new List<Student>();
}