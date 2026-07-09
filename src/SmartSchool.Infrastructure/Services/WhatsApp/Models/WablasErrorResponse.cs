using System.Text.Json.Serialization;

namespace SmartSchool.Infrastructure.Services.WhatsApp.Models;

public class WablasErrorResponse
{
    [JsonPropertyName("status")]
    public bool Status { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}