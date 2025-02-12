using Data;
using Infra.Swagger;
using Microsoft.EntityFrameworkCore;
using Services.Authentication;
using Infra.JWT;

var builder = WebApplication.CreateBuilder(args);

// Configurar o EF Core para PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Registrar serviços de autenticação
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Adicionar configuração do JWT
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization(); // Garantir que o Authorization está registrado

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Substitui a configuração simples pelo método que inclui o suporte ao JWT
builder.Services.AddSwaggerDocumentation();

builder.Services.AddMemoryCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ERP MaxiProd API V1");
    });
}

app.UseAuthentication(); // Middleware de autenticação para validar o JWT
app.UseAuthorization();  // Middleware de autorização

app.MapControllers();
app.Run();