using DTOs.User;

namespace Services.Authentication
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Registra um novo usuário no sistema.
        /// </summary>
        /// <param name="registerDto">Dados de registro do usuário.</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona (opcional).</param>
        /// <returns></returns>
        Task RegisterAsync(RegisterDTO registerDto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Realiza o login do usuário e retorna um token JWT se as credenciais forem válidas.
        /// </summary>
        /// <param name="loginDto">Dados de login do usuário.</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona (opcional).</param>
        /// <returns>Token JWT se a autenticação for bem-sucedida.</returns>
        Task<string> LoginAsync(LoginDTO loginDto, CancellationToken cancellationToken = default);
    }
}