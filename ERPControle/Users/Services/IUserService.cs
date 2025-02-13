using Users.DTO;

namespace Users.Services
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterDto registerDto);
    }
}
