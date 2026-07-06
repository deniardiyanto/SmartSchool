using Microsoft.AspNetCore.Mvc;
using SmartSchool.Application.Features.WhatsApp.Contracts;
using SmartSchool.Application.Features.WhatsApp.Interfaces;

namespace SmartSchool.API.Controllers.Media;

[ApiController]
[Route("api/test/whatsapp")]
public class WhatsAppTestController : ControllerBase
{
    private readonly IWhatsAppService _service;

    public WhatsAppTestController(
        IWhatsAppService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Send()
    {
        var result = await _service.SendAsync(
            new SendWhatsAppRequest
            {
                PhoneNumber = "081234567890",
                Message = "Halo dari SmartSchool"
            });

        return Ok(result);
    }
}