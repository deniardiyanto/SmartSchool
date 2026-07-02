namespace SmartSchool.Application.Features.BarcodeCards.Contracts;

public class PagedBarcodeCardResponse
{
    public IReadOnlyCollection<BarcodeCardDto> Items { get; set; }
        = new List<BarcodeCardDto>();

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalRecords { get; set; }

    public int TotalPages { get; set; }
}