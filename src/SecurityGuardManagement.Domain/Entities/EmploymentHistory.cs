using SecurityGuardManagement.Domain.Common;

namespace SecurityGuardManagement.Domain.Entities;

public class EmploymentHistory : BaseEntity
{
    public int GuardId { get; set; }
    public Guard Guard { get; set; } = null!;
    public string CompanyName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string? CurrentPosition { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Responsibilities { get; set; }
    public string? ReasonForLeaving { get; set; }
}
