using SmartSchool.Domain.Entities;

namespace SmartSchool.Application.Features.BarcodeCards.Contracts;

public class BarcodeCardDto
{
    public Guid Id { get; set; }

    public Guid StudentId { get; set; }

    public string StudentName { get; set; } = string.Empty;

    public string CardNumber { get; set; } = string.Empty;

    public string BarcodeValue { get; set; } = string.Empty;

    public DateTime IssuedDate { get; set; }

    public DateTime? ExpiredDate { get; set; }

    public bool IsActive { get; set; }

    public static BarcodeCardDto FromEntity(BarcodeCard entity)
    {
        return new BarcodeCardDto
        {
            Id = entity.Id,
            StudentId = entity.StudentId,
            StudentName = entity.Student.FullName,
            CardNumber = entity.CardNumber,
            BarcodeValue = entity.BarcodeValue,
            IssuedDate = entity.IssuedDate,
            ExpiredDate = entity.ExpiredDate,
            IsActive = entity.IsActive
        };
    }
}