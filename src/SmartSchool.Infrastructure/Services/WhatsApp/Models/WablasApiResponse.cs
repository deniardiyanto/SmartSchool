namespace SmartSchool.Infrastructure.Services.WhatsApp.Models;

public class WablasApiResponse
{
    public bool Success { get; set; }

    public string MessageId { get; set; } = string.Empty;

    public string ProviderMessage { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
}