using SecurityGuardManagement.Domain.Common;
using SecurityGuardManagement.Domain.ValueObjects;

namespace SecurityGuardManagement.Domain.Entities;

public class Client : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public ContactInformation ContactInfo { get; set; } = new();
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation property
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
