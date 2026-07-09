using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using SmartSchool.Application.Common.Exceptions;
using SmartSchool.Application.Features.WhatsApp.Contracts;
using SmartSchool.Application.Features.WhatsApp.Interfaces;
using SmartSchool.Infrastructure.Constants;
using SmartSchool.Infrastructure.Services.WhatsApp.Helpers;
using SmartSchool.Infrastructure.Services.WhatsApp.Models;

namespace SmartSchool.Infrastructure.Services.WhatsApp;

public class WablasWhatsAppService : IWhatsAppService
{
    private readonly HttpClient _httpClient;

    private readonly ILogger<WablasWhatsAppService> _logger;

    public WablasWhatsAppService(
        HttpClient httpClient,
        ILogger<WablasWhatsAppService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<SendWhatsAppResponse> SendAsync(
        SendWhatsAppRequest request)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            Validate(request);

            request.PhoneNumber =
                PhoneNumberHelper.Normalize(
                    request.PhoneNumber);

            _logger.LogInformation(
                "Sending WhatsApp to {Phone}",
                request.PhoneNumber);

            using var form =
                new MultipartFormDataContent
                {
                    {
                        new StringContent(request.PhoneNumber),
                        "phone"
                    },
                    {
                        new StringContent(request.Message),
                        "message"
                    }
                };

            var httpResponse =
                await _httpClient.PostAsync(
                    WablasApi.SendMessage,
                    form);

            var responseBody =
                await httpResponse.Content.ReadAsStringAsync();

            stopwatch.Stop();

            _logger.LogInformation(
                """
                Wablas Send Message

                Phone      : {Phone}
                HTTP Status: {StatusCode}
                Duration   : {Duration} ms
                Response   : {Response}
                """,
                request.PhoneNumber,
                (int)httpResponse.StatusCode,
                stopwatch.ElapsedMilliseconds,
                responseBody);

            if (!httpResponse.IsSuccessStatusCode)
            {
                return new SendWhatsAppResponse
                {
                    Success = false,

                    MessageId = string.Empty,

                    ProviderMessage = responseBody
                };
            }

            var result =
                JsonSerializer.Deserialize<WablasSendResponse>(
                    responseBody,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            if (result == null)
            {
                return new SendWhatsAppResponse
                {
                    Success = false,

                    MessageId = string.Empty,

                    ProviderMessage =
                        "Failed to deserialize Wablas response."
                };
            }

            var message =
                result.Data.Messages.FirstOrDefault();

            return new SendWhatsAppResponse
            {
                Success = result.Status,

                MessageId =
                    message?.Id ?? string.Empty,

                ProviderMessage =
                    result.Message
            };
        }
        catch (WhatsAppException ex)
        {
            stopwatch.Stop();

            _logger.LogWarning(
                ex,
                "WhatsApp validation failed.");

            return new SendWhatsAppResponse
            {
                Success = false,

                MessageId = string.Empty,

                ProviderMessage = ex.Message
            };
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            _logger.LogError(
                ex,
                "Unexpected error while sending WhatsApp.");

            return new SendWhatsAppResponse
            {
                Success = false,

                MessageId = string.Empty,

                ProviderMessage = ex.Message
            };
        }
    }

    /// <summary>
    /// Validate request.
    /// </summary>
    private static void Validate(
        SendWhatsAppRequest request)
    {
        if (request == null)
        {
            throw new WhatsAppException(
                "Request cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(
            request.PhoneNumber))
        {
            throw new WhatsAppException(
                "Phone number is required.");
        }

        if (string.IsNullOrWhiteSpace(
            request.Message))
        {
            throw new WhatsAppException(
                "Message is required.");
        }
    }
}