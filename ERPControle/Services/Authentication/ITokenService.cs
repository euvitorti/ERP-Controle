using Models.Users;

namespace Services.Authentication
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}