using SecurityGuardManagement.Domain.Common;
using SecurityGuardManagement.Domain.ValueObjects;

namespace SecurityGuardManagement.Domain.Entities;

public class Post : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Address PostAddress { get; set; } = new();
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
    public ICollection<GuardAssignment> Assignments { get; set; } = new List<GuardAssignment>();
}
