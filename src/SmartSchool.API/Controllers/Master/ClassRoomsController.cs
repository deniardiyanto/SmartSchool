using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Responses;
using SmartSchool.Application.Features.ClassRooms.Contracts;
using SmartSchool.Application.Features.ClassRooms.Interfaces;

namespace SmartSchool.API.Controllers.Master;

[ApiController]
[Route("api/master/classrooms")]
public class ClassRoomsController : ControllerBase
{
    private readonly IClassRoomService _service;

    public ClassRoomsController(IClassRoomService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get classrooms with filtering and pagination.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedClassRoomResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] ClassRoomFilterRequest request)
    {
        var result = await _service.GetPagedAsync(request);

        return Ok(ApiResponse<PagedClassRoomResponse>.Ok(result));
    }

    /// <summary>
    /// Get classroom by Id.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ClassRoomDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);

        // Service sebaiknya melempar NotFoundException jika data tidak ada.
        return Ok(ApiResponse<ClassRoomDto>.Ok(result!));
    }

    /// <summary>
    /// Create new classroom.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateClassRoomRequest request)
    {
        var id = await _service.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            ApiResponse<Guid>.Ok(id, "Class room created successfully."));
    }

    /// <summary>
    /// Update classroom.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateClassRoomRequest request)
    {
        await _service.UpdateAsync(id, request);

        return Ok(ApiResponse<string>.Ok(
            "Updated",
            "Class room updated successfully."));
    }

    /// <summary>
    /// Soft delete classroom.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);

        return Ok(ApiResponse<string>.Ok(
            "Deleted",
            "Class room deleted successfully."));
    }
}