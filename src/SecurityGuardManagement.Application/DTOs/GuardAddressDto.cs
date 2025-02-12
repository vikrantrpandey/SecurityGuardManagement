using SecurityGuardManagement.Application.DTOs.Common;

namespace SecurityGuardManagement.Application.DTOs;

public class GuardAddressDto
{
    public int Id { get; set; }
    public int GuardId { get; set; }
    public AddressDto Address { get; set; } = new();
    public bool IsPermanent { get; set; }
    public bool IsCurrentResidence { get; set; }
    public DateTime EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; }
    public bool IsVerified { get; set; }
    public string? VerificationNotes { get; set; }
    public string? Notes { get; set; }
}
