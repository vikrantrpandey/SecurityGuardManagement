using SecurityGuardManagement.Application.Auth.DTOs;
using System.Threading.Tasks;

namespace SecurityGuardManagement.Application.Auth.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    }
}
