namespace SmartSchool.Application.Features.WhatsApp.Contracts;

public class SendWhatsAppResponse
{
    public bool Success { get; set; }

    public string MessageId { get; set; } = string.Empty;

    public string ProviderMessage { get; set; } = string.Empty;
}