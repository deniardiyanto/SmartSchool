using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Responses;
using SmartSchool.Application.Features.Attendances.Contracts;
using SmartSchool.Application.Features.Attendances.Interfaces;

namespace SmartSchool.API.Controllers.Attendance;

[ApiController]
[Route("api/attendance")]
public class AttendancesController : ControllerBase
{
    private readonly IAttendanceService _service;

    public AttendancesController(
        IAttendanceService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get attendance list.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedAttendanceResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromQuery] AttendanceFilterRequest request)
    {
        var result = await _service.GetPagedAsync(request);

        return Ok(ApiResponse<PagedAttendanceResponse>.Ok(
            result,
            "Attendance retrieved successfully."));
    }

    /// <summary>
    /// Get attendance by Id.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<AttendanceDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);

        return Ok(ApiResponse<AttendanceDto>.Ok(
            result,
            "Attendance retrieved successfully."));
    }

    /// <summary>
    /// Create attendance manually.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(
        [FromBody] CreateAttendanceRequest request)
    {
        var id = await _service.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            ApiResponse<Guid>.Ok(
                id,
                "Attendance created successfully."));
    }

    /// <summary>
    /// Update attendance.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateAttendanceRequest request)
    {
        await _service.UpdateAsync(id, request);

        return Ok(ApiResponse<object>.Ok(
            "Updated",
            "Attendance updated successfully."));
    }

    /// <summary>
    /// Soft delete attendance.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);

        return Ok(ApiResponse<object>.Ok(
            "Deleted",
            "Attendance deleted successfully."));
    }
}