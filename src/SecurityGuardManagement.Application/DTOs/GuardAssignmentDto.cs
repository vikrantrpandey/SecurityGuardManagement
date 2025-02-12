using SecurityGuardManagement.Domain.Enums;

namespace SecurityGuardManagement.Application.DTOs;

public class GuardAssignmentDto
{
    public int Id { get; set; }
    public int GuardId { get; set; }
    public int PostId { get; set; }
    public string PostName { get; set; } = string.Empty;
    public AssignmentStatus Status { get; set; }
    public Shift Shift { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Notes { get; set; }
}
