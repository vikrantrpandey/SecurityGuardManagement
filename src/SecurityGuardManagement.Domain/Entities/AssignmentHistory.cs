using SecurityGuardManagement.Domain.Common;
using SecurityGuardManagement.Domain.Enums;

namespace SecurityGuardManagement.Domain.Entities;

public class AssignmentHistory : AuditableEntity
{
    public int GuardAssignmentId { get; set; }
    public GuardAssignment GuardAssignment { get; set; } = null!;
    public AssignmentStatus OldStatus { get; set; }
    public AssignmentStatus NewStatus { get; set; }
    public string? Reason { get; set; }
}
