namespace SmartSchool.Application.Features.Attendances.Scan.Contracts;

public class ScanAttendanceRequest
{
    public string BarcodeValue { get; set; } = string.Empty;
}