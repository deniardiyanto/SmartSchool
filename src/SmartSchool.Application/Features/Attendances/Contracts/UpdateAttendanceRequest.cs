using SmartSchool.Domain.Enums;

namespace SmartSchool.Application.Features.Attendances.Contracts;

public class UpdateAttendanceRequest
{
    public DateTime? CheckInTime { get; set; }

    public DateTime? CheckOutTime { get; set; }

    public AttendanceStatus Status { get; set; }

    public string? Notes { get; set; }
}