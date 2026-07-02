namespace SmartSchool.Application.Features.BarcodeCards.Contracts;

public class CreateBarcodeCardRequest
{
    public Guid StudentId { get; set; }

    public string CardNumber { get; set; } = string.Empty;

    public string BarcodeValue { get; set; } = string.Empty;

    public DateTime IssuedDate { get; set; } = DateTime.UtcNow;

    public DateTime? ExpiredDate { get; set; }
}