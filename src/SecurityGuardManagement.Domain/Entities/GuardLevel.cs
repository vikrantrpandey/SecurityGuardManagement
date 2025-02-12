using SecurityGuardManagement.Domain.Common;

namespace SecurityGuardManagement.Domain.Entities;

public class GuardLevel : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal BaseSalary { get; set; }
    public int AuthorizationLevel { get; set; }
    
    // Navigation property
    public ICollection<Guard> Guards { get; set; } = new List<Guard>();
}
