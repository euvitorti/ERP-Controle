using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Infra.JWT.Config
{
    public static class JwtConfig
    {
        // Método de extensão para adicionar autenticação JWT à aplicação
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // Lê a chave secreta do appsettings.json e converte para bytes
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);

            // Configura o sistema de autenticação usando JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                // Define parâmetros de validação do token JWT
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, // Valida a assinatura do token
                    IssuerSigningKey = new SymmetricSecurityKey(key), // Define a chave secreta para validar
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
