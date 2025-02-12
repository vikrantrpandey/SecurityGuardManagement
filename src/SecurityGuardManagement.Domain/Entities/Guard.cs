using SecurityGuardManagement.Domain.Common;
using SecurityGuardManagement.Domain.Enums;
using SecurityGuardManagement.Domain.ValueObjects;

namespace SecurityGuardManagement.Domain.Entities;

public class Guard : AuditableEntity
{
    public PersonalInformation PersonalInfo { get; set; } = new();
    public ContactInformation ContactInfo { get; set; } = new();
    public GuardStatus Status { get; set; }
    public int GuardLevelId { get; set; }
    public GuardLevel GuardLevel { get; set; } = null!;
    
    // New fields
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
    
    // Navigation properties
    public ICollection<GuardAddress> Addresses { get; set; } = new List<GuardAddress>();
    public ICollection<GuardEducation> Education { get; set; } = new List<GuardEducation>();
    public ICollection<GuardTraining> Training { get; set; } = new List<GuardTraining>();
    public ICollection<EmploymentHistory> EmploymentHistory { get; set; } = new List<EmploymentHistory>();
    public ICollection<GuardAssignment> Assignments { get; set; } = new List<GuardAssignment>();
}
