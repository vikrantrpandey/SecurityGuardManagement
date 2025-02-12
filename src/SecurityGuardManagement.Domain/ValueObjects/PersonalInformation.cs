using SecurityGuardManagement.Domain.Enums;

namespace SecurityGuardManagement.Domain.ValueObjects;

public class PersonalInformation
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
}
