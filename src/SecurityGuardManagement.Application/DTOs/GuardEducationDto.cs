using SecurityGuardManagement.Domain.Enums;

namespace SecurityGuardManagement.Application.DTOs;

public class GuardEducationDto
{
    public int Id { get; set; }
    public int GuardId { get; set; }
    public string Institution { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public EducationLevel Level { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCompleted { get; set; }
    public string? Notes { get; set; }
}
