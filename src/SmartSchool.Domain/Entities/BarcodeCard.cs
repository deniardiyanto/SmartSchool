using SmartSchool.Domain.Common;

namespace SmartSchool.Domain.Entities;

public class BarcodeCard : BaseAuditableEntity
{
    public Guid StudentId { get; set; }

    public string CardNumber { get; set; } = string.Empty;

    public string BarcodeValue { get; set; } = string.Empty;

    public DateTime IssuedDate { get; set; } = DateTime.UtcNow;

    public DateTime? ExpiredDate { get; set; }

    public bool IsActive { get; set; } = true;

    public int PrintedCount { get; set; }

    // Navigation
    public Student Student { get; set; } = null!;

    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
}