using SmartSchool.Domain.Common;
namespace SmartSchool.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Role { get; set; } = "ADMIN";

    public bool IsActive { get; set; } = true;
}