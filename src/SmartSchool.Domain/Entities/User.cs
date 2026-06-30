// using SmartSchool.Domain.Common;

// namespace SmartSchool.Domain.Entities;

// public class User : BaseEntity
// {
//     public string Username { get; set; } = string.Empty;

//     public string PasswordHash { get; set; } = string.Empty;

//     public string FullName { get; set; } = string.Empty;

//     public Guid RoleId { get; set; }

//     public Role Role { get; set; } = null!;

//     public bool IsActive { get; set; } = true;
// }
using SmartSchool.Domain.Common;

namespace SmartSchool.Domain.Entities;

public class User : BaseSoftDeleteEntity
{
    public string Username { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime? LastLogin { get; set; }

    // Foreign Key
    public Guid RoleId { get; set; }

    // Navigation Property
    public Role Role { get; set; } = null!;
}