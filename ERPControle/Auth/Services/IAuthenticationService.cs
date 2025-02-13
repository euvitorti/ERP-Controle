using Auth.DTO;

namespace Auth.Services
{
    public interface IAuthenticationService
    {
        Task<string> LoginAsync(LoginDto loginDto);
    }
}
