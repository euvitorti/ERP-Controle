using Data;
using Infra.Swagger;
using Microsoft.EntityFrameworkCore;
using Infra.JWT.Config;
using System.Text.Json.Serialization;
using Services.Summary;
using Auth.Services;
using People.Services;
using Transactions.Services;
using Infra.JWT.Services;
using Users.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar o EF Core para PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Registrar serviços crud dos users
builder.Services.AddScoped<IUserService, UserService>();

// Registrar serviços de autenticação
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Registrar serviços crud das pessoas
builder.Services.AddScoped<IPersonService, PersonService>();

// Registrar serviços crud das transações
builder.Services.AddScoped<ITransactionService, TransactionService>();

// Registrar serviços crud dos relatórios
builder.Services.AddScoped<ISummaryService, SummaryService>();

// Adicionar configuração do JWT
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddControllers().AddJsonOptions(options =>
    {
        // Converte valores de enum para string
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
