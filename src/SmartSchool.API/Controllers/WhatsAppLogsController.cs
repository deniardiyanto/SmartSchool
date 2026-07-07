using Microsoft.AspNetCore.Mvc;
using SmartSchool.Application.Features.WhatsApp.Contracts;
using SmartSchool.Application.Features.WhatsApp.Interfaces;

namespace SmartSchool.Api.Controllers;

[ApiController]
[Route("api/whatsapp/logs")]
public class WhatsAppLogsController : ControllerBase
{
    private readonly IWhatsAppLogService _service;

    public WhatsAppLogsController(
        IWhatsAppLogService service)
    {
        _service = service;
    }

    /// <summary>
    /// Riwayat pengiriman WhatsApp
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedWhatsAppLogResponse>> GetPaged(
        [FromQuery] WhatsAppLogFilterRequest request)
    {
        var result = await _service.GetPagedAsync(request);

        return Ok(result);
    }

    /// <summary>
    /// Detail WhatsApp Log
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<WhatsAppLogDto>> GetById(
        Guid id)
    {
        var result = await _service.GetByIdAsync(id);

        return Ok(result);
    }
}