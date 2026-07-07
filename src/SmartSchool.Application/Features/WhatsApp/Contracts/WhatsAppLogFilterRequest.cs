namespace SmartSchool.Application.Features.WhatsApp.Contracts;

public class WhatsAppLogFilterRequest
{
    public string? StudentName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Status { get; set; }

    public DateOnly? FromDate { get; set; }

    public DateOnly? ToDate { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 20;
}