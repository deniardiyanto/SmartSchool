namespace SmartSchool.Application.Features.BarcodeCards.Contracts;

public class BarcodeCardFilterRequest
{
    public string? Search { get; set; }

    public bool? IsActive { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}