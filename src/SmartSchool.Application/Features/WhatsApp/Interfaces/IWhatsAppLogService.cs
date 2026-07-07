using SmartSchool.Application.Features.WhatsApp.Contracts;

namespace SmartSchool.Application.Features.WhatsApp.Interfaces;

public interface IWhatsAppLogService
{
    Task<PagedWhatsAppLogResponse> GetPagedAsync(
        WhatsAppLogFilterRequest request);

    Task<WhatsAppLogDto> GetByIdAsync(
        Guid id);

    Task<RetryWhatsAppResponse> RetryAsync(
        Guid id);
}