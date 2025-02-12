namespace SecurityGuardManagement.Application.DTOs;

public class GuardTrainingDto
{
    public int Id { get; set; }
    public int GuardId { get; set; }
    public string TrainingName { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public string CertificationNumber { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public bool IsCompleted { get; set; }
    public string? Notes { get; set; }
}
