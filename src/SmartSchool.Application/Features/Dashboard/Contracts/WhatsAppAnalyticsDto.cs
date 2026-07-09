namespace SmartSchool.Application.Features.Dashboard.Contracts;

public class WhatsAppAnalyticsDto
{
    public int Success { get; set; }

    public int Failed { get; set; }

    public int Pending { get; set; }

    public decimal SuccessRate { get; set; }
}