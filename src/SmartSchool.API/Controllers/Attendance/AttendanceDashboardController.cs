using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Responses;
using SmartSchool.Application.Features.Attendances.Dashboard.Contracts;
using SmartSchool.Application.Features.Attendances.Dashboard.Interfaces;

namespace SmartSchool.API.Controllers.Attendance;

[ApiController]
[Route("api/attendance/dashboard")]
public class AttendanceDashboardController : ControllerBase
{
    private readonly IAttendanceDashboardService _service;

    public AttendanceDashboardController(
        IAttendanceDashboardService service)
    {
        _service = service;
    }

    [HttpGet("today")]
    [ProducesResponseType(
        typeof(ApiResponse<AttendanceDashboardDto>),
        StatusCodes.Status200OK)]
    public async Task<IActionResult> Today()
    {
        var result = await _service.GetTodaySummaryAsync();

        return Ok(ApiResponse<AttendanceDashboardDto>.Ok(
            result,
            "Dashboard loaded."));
    }
}