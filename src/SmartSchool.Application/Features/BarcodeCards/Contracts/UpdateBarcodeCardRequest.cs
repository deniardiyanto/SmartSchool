namespace SmartSchool.Application.Features.BarcodeCards.Contracts;

public class UpdateBarcodeCardRequest
{
    public string CardNumber { get; set; } = string.Empty;

    public string BarcodeValue { get; set; } = string.Empty;

    public DateTime IssuedDate { get; set; }

    public DateTime? ExpiredDate { get; set; }

    public bool IsActive { get; set; }
}