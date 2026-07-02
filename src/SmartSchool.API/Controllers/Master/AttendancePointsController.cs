using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Responses;
using SmartSchool.Application.Features.AttendancePoints.Contracts;
using SmartSchool.Application.Features.AttendancePoints.Interfaces;

namespace SmartSchool.API.Controllers.Master;

[ApiController]
[Route("api/master/attendance-points")]
public class AttendancePointsController : ControllerBase
{
    private readonly IAttendancePointService _service;

    public AttendancePointsController(
        IAttendancePointService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] AttendancePointFilterRequest request)
    {
        var result = await _service.GetPagedAsync(request);

        return Ok(ApiResponse<PagedAttendancePointResponse>.Ok(
            result,
            "Attendance Points retrieved successfully."));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);

        return Ok(ApiResponse<AttendancePointDto>.Ok(
            result,
            "Attendance Point retrieved successfully."));
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateAttendancePointRequest request)
    {
        var id = await _service.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            ApiResponse<Guid>.Ok(
                id,
                "Attendance Point created successfully."));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateAttendancePointRequest request)
    {
        await _service.UpdateAsync(id, request);

        return Ok(ApiResponse<object>.Ok(
            "Updated",
            "Attendance Point updated successfully."));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);

        return Ok(ApiResponse<object>.Ok(
            "Deleted",
            "Attendance Point deleted successfully."));
    }
}