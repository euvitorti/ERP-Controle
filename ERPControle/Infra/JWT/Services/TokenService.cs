using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Users.Model;

namespace Infra.JWT.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _jwtKey;

        public TokenService(IConfiguration configuration)
        {
            // Obtém a chave JWT da configuração e lança exceção se a chave não estiver definida
            _jwtKey = configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key", "A chave JWT é obrigatória");
        }

        // Gera um token JWT para o usuário autenticado
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Converte a chave JWT em bytes para utilização na assinatura
            var key = Encoding.ASCII.GetBytes(_jwtKey);

            // Define as informações que estarão contidas no token
            var claims = new[] {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            // Configura as credenciais de assinatura utilizando a chave
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            // Define a hora de expiração e credenciais
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = signingCredentials
            };

            // Cria o token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Retorna o token serializado em string
            return tokenHandler.WriteToken(token);
        }
    }
}
