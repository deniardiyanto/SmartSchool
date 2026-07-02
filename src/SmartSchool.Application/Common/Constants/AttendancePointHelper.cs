using SmartSchool.Domain.Enums;

namespace SmartSchool.Application.Common.Constants;

public static class AttendancePointHelper
{
    public static int GetPoint(
        AttendanceStatus status)
    {
        return status switch
        {
            AttendanceStatus.Present => AttendancePointConstant.OnTime,

            AttendanceStatus.Late => AttendancePointConstant.Late,

            _ => 0
        };
    }

    public static string GetReason(
        AttendanceStatus status)
    {
        return status switch
        {
            AttendanceStatus.Present => "On Time",

            AttendanceStatus.Late => "Late",

            _ => "Attendance"
        };
    }
}