using SecurityGuardManagement.Domain.Common;

namespace SecurityGuardManagement.Domain.Entities;

public class GuardTraining : BaseEntity
{
    public int GuardId { get; set; }
    public Guard Guard { get; set; } = null!;
    public string TrainingName { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? CertificationNumber { get; set; }
}
