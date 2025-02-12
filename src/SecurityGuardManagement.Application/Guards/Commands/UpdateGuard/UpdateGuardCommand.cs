using MediatR;
using SecurityGuardManagement.Domain.Enums;

namespace SecurityGuardManagement.Application.Guards.Commands.UpdateGuard;

public record UpdateGuardCommand : IRequest<Unit>
{
    public int Id { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public Gender Gender { get; init; }
    public string Phone { get; init; } = string.Empty;
    public string? Email { get; init; }
    public GuardStatus Status { get; init; }
    public int GuardLevelId { get; init; }
    public string GuardIdNumber { get; init; } = string.Empty;
    public MaritalStatus MaritalStatus { get; init; }
    public string Nationality { get; init; } = string.Empty;
    public DateTime ApplicationDate { get; init; }
    public DateTime? InterviewDate { get; init; }
    public DateTime? DeployedDate { get; init; }
    public string? DependentContactDetails { get; init; }
    public string? DependentRelations { get; init; }
    public string? BankAccountDetails { get; init; }
    public string? PANNumber { get; init; }
    public string? SocialSecurityNumber { get; init; }
    public string? SkillsCertifications { get; init; }
    public bool PoliceReportStatus { get; init; }
    public bool DocumentsSubmittedStatus { get; init; }
}
