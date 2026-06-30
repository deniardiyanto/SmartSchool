using SmartSchool.Domain.Common;

namespace SmartSchool.Domain.Entities;

public class WhatsAppLog : BaseAuditableEntity
{
    public Guid AttendanceId { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Pending / Success / Failed
    /// </summary>
    public string Status { get; set; } = "Pending";

    public string? ProviderResponse { get; set; }

    public DateTime? SentAt { get; set; }

    // Navigation
    public Attendance Attendance { get; set; } = null!;
}