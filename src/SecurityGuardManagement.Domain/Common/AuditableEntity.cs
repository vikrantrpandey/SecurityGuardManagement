namespace SecurityGuardManagement.Domain.Common;

public abstract class AuditableEntity : BaseEntity
{
    public string CreatedBy { get; set; } = string.Empty;
    public string? LastModifiedBy { get; set; }
}
