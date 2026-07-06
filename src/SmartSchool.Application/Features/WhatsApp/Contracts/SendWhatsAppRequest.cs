namespace SmartSchool.Application.Features.WhatsApp.Contracts;

public class SendWhatsAppRequest
{
    public string PhoneNumber { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;
}