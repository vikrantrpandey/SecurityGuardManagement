using SecurityGuardManagement.Application.DTOs.Common;
using SecurityGuardManagement.Domain.Enums;

namespace SecurityGuardManagement.Application.DTOs;

public class GuardDto
{
    public int Id { get; set; }
    public PersonalInformationDto PersonalInfo { get; set; } = new();
    public ContactInformationDto ContactInfo { get; set; } = new();
    public GuardStatus Status { get; set; }
    public int GuardLevelId { get; set; }
    public string GuardIdNumber { get; set; } = string.Empty;
    public MaritalStatus MaritalStatus { get; set; }
    public string Nationality { get; set; } = string.Empty;
    public DateTime ApplicationDate { get; set; }
    public DateTime? InterviewDate { get; set; }
    public DateTime? DeployedDate { get; set; }
    public string? DependentContactDetails { get; set; }
    public string? DependentRelations { get; set; }
    public string? BankAccountDetails { get; set; }
    public string? PANNumber { get; set; }
    public string? SocialSecurityNumber { get; set; }
    public string? SkillsCertifications { get; set; }
    public bool PoliceReportStatus { get; set; }
    public bool DocumentsSubmittedStatus { get; set; }
    public List<GuardAddressDto> Addresses { get; set; } = new();
    public List<EmploymentHistoryDto> EmploymentHistory { get; set; } = new();
    public List<GuardTrainingDto> GuardTraining { get; set; } = new();
    public List<GuardAssignmentDto> GuardAssignments { get; set; } = new();
}
