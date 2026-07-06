using SmartSchool.Application.Features.WhatsApp.Contracts;

namespace SmartSchool.Application.Features.WhatsApp.Interfaces;

public interface IWhatsAppService
{
    Task<SendWhatsAppResponse> SendAsync(
        SendWhatsAppRequest request);
}