namespace SmartSchool.Application.Features.Attendances.Scan.Contracts;

// public class ScanAttendanceResponse
// {
//     public Guid AttendanceId { get; set; }

//     public Guid StudentId { get; set; }

//     public string StudentName { get; set; } = string.Empty;

//     public string ClassRoomName { get; set; } = string.Empty;

//     public string BarcodeValue { get; set; } = string.Empty;

//     /// <summary>
//     /// CheckIn / CheckOut
//     /// </summary>
//     public string ScanType { get; set; } = string.Empty;

//     public DateTime ScanTime { get; set; }

//     public string Status { get; set; } = string.Empty;
// }
public class ScanAttendanceResponse
{
    public Guid AttendanceId { get; set; }

    public Guid StudentId { get; set; }

    public string StudentName { get; set; } = "";

    public string ClassRoomName { get; set; } = "";

    public string BarcodeValue { get; set; } = "";

    public string ScanType { get; set; } = "";

    public DateTime ScanTime { get; set; }

    public string Status { get; set; } = "";

    public int Point { get; set; }

    public string Reason { get; set; } = "";
}