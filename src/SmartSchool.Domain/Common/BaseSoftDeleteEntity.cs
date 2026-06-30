namespace SmartSchool.Domain.Common;

public abstract class BaseSoftDeleteEntity : BaseAuditableEntity
{
    public bool IsDeleted { get; set; } = false;
}