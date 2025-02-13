using Auth.DTO;
using Data;
using Infra.JWT.Services;
using Microsoft.EntityFrameworkCore;

namespace Auth.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthenticationService(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // Realiza o login do usuário e retorna um token JWT caso as credenciais sejam válidas.
        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            // Busca o usuário pelo Email de forma assíncrona.
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            
            // Verifica se o usuário foi encontrado e se a senha informada corresponde à senha armazenada.
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Senha, user.PasswordHash))
                throw new UnauthorizedAccessException("Usuário ou senha inválidos.");

            // Gera e retorna o token JWT utilizando o serviço de token.
            return _tokenService.GenerateToken(user);
        }
    }
}
