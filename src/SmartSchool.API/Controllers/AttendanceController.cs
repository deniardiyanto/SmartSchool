using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Responses;
using SmartSchool.Application.Features.Attendances.Scan.Contracts;
using SmartSchool.Application.Features.Attendances.Scan.Interfaces;

namespace SmartSchool.API.Controllers;

[ApiController]
[Route("api/attendance")]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceScannerService _scannerService;

    public AttendanceController(
        IAttendanceScannerService scannerService)
    {
        _scannerService = scannerService;
    }

    /// <summary>
    /// Scan barcode student (Check-In / Check-Out)
    /// </summary>
    [HttpPost("scan")]
    [ProducesResponseType(
        typeof(ApiResponse<ScanAttendanceResponse>),
        StatusCodes.Status200OK)]
    [ProducesResponseType(
        typeof(ApiResponse<object>),
        StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Scan(
        [FromBody] ScanAttendanceRequest request)
    {
        var result = await _scannerService.ScanAsync(request);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}