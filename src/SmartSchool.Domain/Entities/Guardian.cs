using SmartSchool.Domain.Common;
using SmartSchool.Domain.Enums;

namespace SmartSchool.Domain.Entities;

public class Guardian : BaseSoftDeleteEntity
{
    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string? Email { get; set; }

   public GuardianRelationship Relationship { get; set; }

    public ICollection<Student> Students { get; set; }
        = new List<Student>();
}