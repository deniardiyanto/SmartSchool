using SmartSchool.Application.Common.Models;
using SmartSchool.Application.Features.Attendances.Scan.Contracts;

namespace SmartSchool.Application.Features.Attendances.Scan.Interfaces;

public interface IAttendanceScannerService
{
    Task<ApiResponse<ScanAttendanceResponse>> ScanAsync(
        ScanAttendanceRequest request);
}