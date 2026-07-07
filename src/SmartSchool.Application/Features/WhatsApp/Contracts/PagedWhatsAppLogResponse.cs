namespace SmartSchool.Application.Features.WhatsApp.Contracts;

public class PagedWhatsAppLogResponse
{
    public List<WhatsAppLogDto> Items { get; set; }
        = new();

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalPages { get; set; }

    public int TotalRecords { get; set; }
}