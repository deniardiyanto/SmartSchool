namespace SmartSchool.Application.Features.WhatsApp.Contracts;

public class RetryWhatsAppResponse
{
    public bool Success { get; set; }

    public string Status { get; set; } = string.Empty;

    public string ProviderResponse { get; set; } = string.Empty;

    public DateTime? SentAt { get; set; }
}