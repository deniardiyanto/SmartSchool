// using SmartSchool.Domain.Common;

// namespace SmartSchool.Domain.Entities;

// public class AttendanceRule : BaseAuditableEntity
// {
//     public string RuleName { get; set; } = "Default";

//     /// <summary>
//     /// Jam mulai masuk sekolah
//     /// </summary>
//     public TimeSpan StartTime { get; set; }

//     /// <summary>
//     /// Setelah jam ini dianggap terlambat
//     /// </summary>
//     public TimeSpan LateTime { get; set; }

//     /// <summary>
//     /// Setelah jam ini dianggap alpha jika belum scan
//     /// </summary>
//     public TimeSpan AlphaTime { get; set; }

//     public int LatePoint { get; set; } = -5;

//     public bool EnableWhatsapp { get; set; } = true;

//     public bool AllowMultipleScan { get; set; }

//     public string? Notes { get; set; }

//     public bool IsActive { get; set; } = true;

//     // Navigation
//     public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
// }
using SmartSchool.Domain.Common;

namespace SmartSchool.Domain.Entities;

public class AttendanceRule : BaseAuditableEntity
{
    public string RuleName { get; set; } = string.Empty;

    public TimeOnly CheckInStart { get; set; }

    public TimeOnly CheckInEnd { get; set; }

    public TimeOnly CheckOutStart { get; set; }

    public TimeOnly CheckOutEnd { get; set; }

    public int LateToleranceMinutes { get; set; }

    public int PresentPoint { get; set; }

    public int LatePoint { get; set; }

    public int AbsentPoint { get; set; }

    public int EarlyCheckoutPoint { get; set; }

    public bool IsActive { get; set; } = true;
}