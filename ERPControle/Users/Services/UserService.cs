using Data;
using Infra.JWT.Services;
using Microsoft.EntityFrameworkCore;
using Users.DTO;
using Users.Model;

namespace Users.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public async Task RegisterAsync(RegisterDto registerDto)
        {
            // Verifica se já existe um usuário com o mesmo Email
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
                throw new Exception("Usuário já existe.");

            var user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,

                // Criptografa a senha utilizando BCrypt
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Senha)
            };

            // Adiciona o novo usuário ao contexto do Entity Framework
            _context.Users.Add(user);

            // Salva no banco de dados
            await _context.SaveChangesAsync();
        }
    }
}
