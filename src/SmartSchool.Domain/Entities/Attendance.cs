// using SmartSchool.Domain.Common;
// using SmartSchool.Domain.Enums;

// namespace SmartSchool.Domain.Entities;

// public class Attendance : BaseAuditableEntity
// {
//     public Guid StudentId { get; set; }

//     public Guid BarcodeCardId { get; set; }

//     public Guid AttendanceRuleId { get; set; }

//     public DateTime AttendanceDate { get; set; }

//     public DateTime ScanTime { get; set; }

//     public AttendanceType AttendanceType { get; set; }

//     public AttendanceStatus Status { get; set; }

//     public int LateMinutes { get; set; }

//     public int PointDeduction { get; set; }

//     public Guid ScannedBy { get; set; }

//     public string? Notes { get; set; }

//     // Navigation
//     public Student Student { get; set; } = null!;

//     public BarcodeCard BarcodeCard { get; set; } = null!;

//     public AttendanceRule AttendanceRule { get; set; } = null!;

//     public ICollection<AttendancePoint> AttendancePoints { get; set; } = new List<AttendancePoint>();

//     public ICollection<WhatsAppLog> WhatsAppLogs { get; set; } = new List<WhatsAppLog>();
// }
using SmartSchool.Domain.Common;
using SmartSchool.Domain.Enums;

namespace SmartSchool.Domain.Entities;

public class Attendance : BaseAuditableEntity
{
    public Guid StudentId { get; set; }

    public DateOnly AttendanceDate { get; set; }

    public DateTime? CheckInTime { get; set; }

    public DateTime? CheckOutTime { get; set; }

    public AttendanceStatus Status { get; set; }

    public string? Notes { get; set; }

    public Guid? BarcodeCardId { get; set; }

    // Navigation
    public Student Student { get; set; } = null!;

    public BarcodeCard? BarcodeCard { get; set; }
     // Tambahkan kembali
    public ICollection<AttendancePoint> AttendancePoints { get; set; }
        = new List<AttendancePoint>();

    public ICollection<WhatsAppLog> WhatsAppLogs { get; set; }
        = new List<WhatsAppLog>();
}