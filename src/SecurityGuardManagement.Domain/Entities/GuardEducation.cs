using SecurityGuardManagement.Domain.Common;
using SecurityGuardManagement.Domain.Enums;

namespace SecurityGuardManagement.Domain.Entities;

public class GuardEducation : BaseEntity
{
    public int GuardId { get; set; }
    public Guard Guard { get; set; } = null!;
    public EducationLevel Level { get; set; }
    public string Institution { get; set; } = string.Empty;
    public string? Major { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
