using SmartSchool.Domain.Common;

namespace SmartSchool.Domain.Entities;

public class SchoolSetting : BaseAuditableEntity
{
    public string SchoolName { get; set; } = string.Empty;

    public string? SchoolAddress { get; set; }

    public string? SchoolPhone { get; set; }

    public string? PrincipalName { get; set; }

    /// <summary>
    /// Contoh : 07:00
    /// </summary>
    public TimeSpan AttendanceStartTime { get; set; }

    /// <summary>
    /// Contoh : 08:00
    /// </summary>
    public TimeSpan LateTime { get; set; }

    /// <summary>
    /// Contoh : 08:30
    /// </summary>
    public TimeSpan AlphaTime { get; set; }

    public int LatePoint { get; set; } = -5;

    public bool EnableWhatsapp { get; set; } = true;
}