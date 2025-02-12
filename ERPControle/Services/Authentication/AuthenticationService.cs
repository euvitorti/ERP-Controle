using DTOs.User;
using Data;
using Microsoft.EntityFrameworkCore;
using Models.Users;

namespace Services.Authentication
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

        // Registra um novo usuário no sistema.
        public async Task RegisterAsync(RegisterDTO registerDto)
        {
            // Verifica se já existe um usuário com o mesmo Email utilizando uma operação assíncrona.
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
                throw new Exception("Usuário já existe.");

            // Cria um novo objeto de usuário com os dados fornecidos.
            var user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                // Criptografa a senha utilizando BCrypt para armazená-la de forma segura.
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };

            // Adiciona o novo usuário ao contexto do Entity Framework.
            _context.Users.Add(user);
            // Persiste as alterações no banco de dados.
            await _context.SaveChangesAsync();
        }

        // Realiza o login do usuário e retorna um token JWT caso as credenciais sejam válidas.
        // <returns>Token JWT se a autenticação for bem-sucedida.</returns>
        public async Task<string> LoginAsync(LoginDTO loginDto)
        {
            // Busca o usuário pelo Email de forma assíncrona.
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            
            // Verifica se o usuário foi encontrado e se a senha informada corresponde à senha armazenada.
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Usuário ou senha inválidos.");

            // Gera e retorna o token JWT utilizando o serviço de token.
            return _tokenService.GenerateToken(user);
        }
    }
}