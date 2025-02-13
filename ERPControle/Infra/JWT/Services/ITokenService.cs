using Users.Model;

namespace Infra.JWT.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}