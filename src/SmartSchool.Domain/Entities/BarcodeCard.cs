using SmartSchool.Domain.Common;

namespace SmartSchool.Domain.Entities;

public class BarcodeCard : BaseSoftDeleteEntity
{
    public string Barcode { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime? ExpiredDate { get; set; }

    public Guid StudentId { get; set; }

    public Student Student { get; set; } = null!;
}