using SecurityGuardManagement.Domain.Common;
using SecurityGuardManagement.Domain.Enums;
using SecurityGuardManagement.Domain.ValueObjects;

namespace SecurityGuardManagement.Domain.Entities;

public class Client : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public ContactInformation ContactInfo { get; set; } = new();
    public ClientStatus Status { get; set; }
    public string? Description { get; set; }
    // Navigation property
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
