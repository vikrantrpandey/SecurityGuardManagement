using SecurityGuardManagement.Domain.Common;
using SecurityGuardManagement.Domain.ValueObjects;

namespace SecurityGuardManagement.Domain.Entities;

public class GuardAddress : BaseEntity
{
    public int GuardId { get; set; }
    public Guard Guard { get; set; } = null!;
    public Address Address { get; set; } = new();
    public bool IsCurrent { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
