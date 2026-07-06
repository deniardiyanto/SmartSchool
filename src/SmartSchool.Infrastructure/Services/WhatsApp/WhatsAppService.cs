using Microsoft.Extensions.Options;
using SmartSchool.Application.Features.WhatsApp.Contracts;
using SmartSchool.Application.Features.WhatsApp.Interfaces;
using SmartSchool.Infrastructure.Configuration;

namespace SmartSchool.Infrastructure.Services.WhatsApp;

public class WhatsAppService : IWhatsAppService
{
    private readonly HttpClient _httpClient;
    private readonly FonnteOptions _options;

    public WhatsAppService(
        HttpClient httpClient,
        IOptions<FonnteOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;

        _httpClient.BaseAddress = new Uri(_options.BaseUrl);

        _httpClient.DefaultRequestHeaders.Add(
            "Authorization",
            _options.Token);
    }

    private static string NormalizePhone(string phone)
    {
        phone = phone.Trim()
                     .Replace(" ", "")
                     .Replace("-", "");

        if (phone.StartsWith("08"))
        {
            phone = "628" + phone.Substring(2);
        }

        return phone;
    }

    public async Task<SendWhatsAppResponse> SendAsync(
        SendWhatsAppRequest request)
    {
        var payload = new Dictionary<string, string>
        {
            { "target", NormalizePhone(request.PhoneNumber) },
            { "message", request.Message }
        };

        var content = new FormUrlEncodedContent(payload);

        var response = await _httpClient.PostAsync(
            "/send",
            content);

        var body = await response.Content.ReadAsStringAsync();

        return new SendWhatsAppResponse
        {
            Success = response.IsSuccessStatusCode,
            MessageId = Guid.NewGuid().ToString(),
            ProviderMessage = body
        };
    }
}