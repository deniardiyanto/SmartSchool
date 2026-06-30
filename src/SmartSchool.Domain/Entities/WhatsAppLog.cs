using SmartSchool.Domain.Common;
using SmartSchool.Domain.Enums;

namespace SmartSchool.Domain.Entities;

public class WhatsAppLog : BaseAuditableEntity
{
    public string PhoneNumber { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public WhatsAppStatus Status { get; set; }

    public string? Response { get; set; }

    public DateTime SentAt { get; set; }
}