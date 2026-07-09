// using Microsoft.AspNetCore.Mvc;
// using SmartSchool.Application.Features.WhatsApp.Contracts;
// using SmartSchool.Application.Features.WhatsApp.Interfaces;

// namespace SmartSchool.API.Controllers.Media;

// [ApiController]
// [Route("api/test/whatsapp")]
// public class WhatsAppTestController : ControllerBase
// {
//     private readonly IWhatsAppService _service;

//     public WhatsAppTestController(
//         IWhatsAppService service)
//     {
//         _service = service;
//     }

//     [HttpPost]
//     public async Task<IActionResult> Send()
//     {
//         var result = await _service.SendAsync(
//             new SendWhatsAppRequest
//             {
//                 PhoneNumber = "088980024009",
//                 Message = "Halo dari SmartSchool"
//             });

//         return Ok(result);
//     }
// }
using Microsoft.AspNetCore.Mvc;
using SmartSchool.Application.Features.WhatsApp.Contracts;
using SmartSchool.Application.Features.WhatsApp.Interfaces;

namespace SmartSchool.API.Controllers.Media;

[ApiController]
[Route("api/test/whatsapp")]
public class TestWhatsAppController : ControllerBase
{
    private readonly IWhatsAppService _whatsAppService;

    public TestWhatsAppController(
        IWhatsAppService whatsAppService)
    {
        _whatsAppService = whatsAppService;
    }

    /// <summary>
    /// Test kirim WhatsApp melalui Wablas.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Send(
        [FromBody] SendWhatsAppRequest request)
    {
        var result = await _whatsAppService.SendAsync(request);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}