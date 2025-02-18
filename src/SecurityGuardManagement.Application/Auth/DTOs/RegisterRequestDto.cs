using System.ComponentModel.DataAnnotations;
using SecurityGuardManagement.Domain.Enums;

namespace SecurityGuardManagement.Application.Auth.DTOs
{
    public class RegisterRequestDto
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
