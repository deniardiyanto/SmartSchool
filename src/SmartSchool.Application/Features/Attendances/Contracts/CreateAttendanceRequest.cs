using SmartSchool.Domain.Enums;

namespace SmartSchool.Application.Features.Attendances.Contracts;

public class CreateAttendanceRequest
{
    public Guid StudentId { get; set; }

    public DateOnly AttendanceDate { get; set; }

    public DateTime? CheckInTime { get; set; }

    public DateTime? CheckOutTime { get; set; }

    public AttendanceStatus Status { get; set; }

    public string? Notes { get; set; }

    public Guid? BarcodeCardId { get; set; }
}