namespace SecurityGuardManagement.Application.DTOs;

public class EmploymentHistoryDto
{
    public int Id { get; set; }
    public int GuardId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string CurrentPosition { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Responsibilities { get; set; }
    public string? ReasonForLeaving { get; set; }
    public string? ReferenceContact { get; set; }
    public bool IsVerified { get; set; }
    public string? VerificationNotes { get; set; }
}
