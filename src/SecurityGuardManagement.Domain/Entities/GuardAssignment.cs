using SecurityGuardManagement.Domain.Common;
using SecurityGuardManagement.Domain.Enums;

namespace SecurityGuardManagement.Domain.Entities;

public class GuardAssignment : AuditableEntity
{
    public int GuardId { get; set; }
    public Guard Guard { get; set; } = null!;
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public AssignmentStatus Status { get; set; }
    public Shift Shift { get; set; }
    public string? Notes { get; set; }
    
    // Navigation property
    public ICollection<AssignmentHistory> History { get; set; } = new List<AssignmentHistory>();
}
