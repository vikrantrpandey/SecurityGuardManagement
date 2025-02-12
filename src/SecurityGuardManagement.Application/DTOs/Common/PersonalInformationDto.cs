using SecurityGuardManagement.Domain.Enums;

namespace SecurityGuardManagement.Application.DTOs.Common;

public class PersonalInformationDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
}
