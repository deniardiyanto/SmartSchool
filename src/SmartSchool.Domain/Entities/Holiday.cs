using SmartSchool.Domain.Common;

namespace SmartSchool.Domain.Entities;

public class Holiday : BaseAuditableEntity
{
    public DateOnly HolidayDate { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsNationalHoliday { get; set; }
}