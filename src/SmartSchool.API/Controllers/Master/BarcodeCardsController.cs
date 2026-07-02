using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Responses;
using SmartSchool.Application.Features.BarcodeCards.Contracts;
using SmartSchool.Application.Features.BarcodeCards.Interfaces;

namespace SmartSchool.API.Controllers.Master;

[ApiController]
[Route("api/master/barcodecards")]
public class BarcodeCardsController : ControllerBase
{
    private readonly IBarcodeCardService _service;

    public BarcodeCardsController(
        IBarcodeCardService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all barcode cards.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedBarcodeCardResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromQuery] BarcodeCardFilterRequest request)
    {
        var result = await _service.GetPagedAsync(request);

        return Ok(ApiResponse<PagedBarcodeCardResponse>.Ok(
            result,
            "Barcode cards retrieved successfully."));
    }

    /// <summary>
    /// Get barcode card by Id.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<BarcodeCardDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);

        return Ok(ApiResponse<BarcodeCardDto>.Ok(
            result,
            "Barcode card retrieved successfully."));
    }

    /// <summary>
    /// Create barcode card.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(
        [FromBody] CreateBarcodeCardRequest request)
    {
        var id = await _service.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            ApiResponse<Guid>.Ok(
                id,
                "Barcode card created successfully."));
    }

    /// <summary>
    /// Update barcode card.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateBarcodeCardRequest request)
    {
        await _service.UpdateAsync(id, request);

        return Ok(ApiResponse<object>.Ok(
            "Updated",
            "Barcode card updated successfully."));
    }

    /// <summary>
    /// Delete barcode card.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);

        return Ok(ApiResponse<object>.Ok(
            "Deleted",
            "Barcode card deleted successfully."));
    }
}