using DTOs.User;

namespace Services.Authentication
{
    public interface IAuthenticationService
    {
        // Registra um novo usuário no sistema.
        // <returns>Mensagem informando se o user foi ou não salvo</returns>
        Task RegisterAsync(RegisterDTO registerDto);

        // Realiza o login do usuário e retorna um token JWT se as credenciais forem válidas.
        // <returns>Token JWT se a autenticação for bem-sucedida.</returns>
        Task<string> LoginAsync(LoginDTO loginDto);
    }
}