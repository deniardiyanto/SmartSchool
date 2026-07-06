using SmartSchool.Application.Features.WhatsApp.Contracts;
using SmartSchool.Application.Features.WhatsApp.Interfaces;

namespace SmartSchool.Infrastructure.Services.WhatsApp;

public class WhatsAppService : IWhatsAppService
{
    public async Task<SendWhatsAppResponse> SendAsync(
        SendWhatsAppRequest request)
    {
        await Task.Delay(300);

        return new SendWhatsAppResponse
        {
            Success = true,
            MessageId = Guid.NewGuid().ToString(),
            ProviderMessage = "Dummy WhatsApp sent successfully."
        };
    }
}