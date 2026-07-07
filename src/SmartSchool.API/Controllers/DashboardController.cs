using Microsoft.AspNetCore.Mvc;
using SmartSchool.Application.Features.Dashboard.Contracts;
using SmartSchool.Application.Features.Dashboard.Interfaces;

namespace SmartSchool.Api.Controllers;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _service;

    public DashboardController(
        IDashboardService service)
    {
        _service = service;
    }

    /// <summary>
    /// Dashboard attendance hari ini
    /// </summary>
    [HttpGet("attendance")]
    public async Task<ActionResult<AttendanceDashboardResponse>>
        GetAttendanceDashboard()
    {
        var result =
            await _service.GetAttendanceDashboardAsync();

        return Ok(result);
    }
}