using SmartSchool.Domain.Common;
using SmartSchool.Domain.Enums;

namespace SmartSchool.Domain.Entities;

public class Student : BaseSoftDeleteEntity
{
    public string NIS { get; set; } = string.Empty;

    public string? NISN { get; set; }

    public string FullName { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; }

   public Gender Gender { get; set; }
    public string? Address { get; set; }

    public bool IsActive { get; set; } = true;

    public Guid GuardianId  { get; set; }

    public Guardian Guardian { get; set; } = null!;

    public Guid ClassRoomId { get; set; }

    public ClassRoom ClassRoom { get; set; } = null!;
    public string? PhotoPath { get; set; }

    public ICollection<Attendance> Attendances { get; set; }
        = new List<Attendance>();

    public ICollection<AttendancePoint> AttendancePoints { get; set; }
        = new List<AttendancePoint>();

    public ICollection<BarcodeCard> BarcodeCards { get; set; }
        = new List<BarcodeCard>();
}