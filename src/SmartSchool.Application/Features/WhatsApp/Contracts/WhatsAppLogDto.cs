using SmartSchool.Domain.Entities;

namespace SmartSchool.Application.Features.WhatsApp.Contracts;

public class WhatsAppLogDto
{
    public Guid Id { get; set; }

    public Guid AttendanceId { get; set; }

    public string StudentName { get; set; } = string.Empty;

    public string ClassRoom { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string? ProviderResponse { get; set; }

    public DateTime? SentAt { get; set; }

    public static WhatsAppLogDto FromEntity(
        WhatsAppLog entity)
    {
        return new WhatsAppLogDto
        {
            Id = entity.Id,

            AttendanceId = entity.AttendanceId,

            StudentName =
                entity.Attendance.Student.FullName,

            ClassRoom =
                entity.Attendance.Student.ClassRoom.Name,

            PhoneNumber = entity.PhoneNumber,

            Message = entity.Message,

            Status = entity.Status,

            ProviderResponse =
                entity.ProviderResponse,

            SentAt = entity.SentAt
        };
    }
}