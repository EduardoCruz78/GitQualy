using Microsoft.EntityFrameworkCore;
using qualyteam_api.Application.Interfaces;
using qualyteam_api.Application.Services;
using qualyteam_api.Domain.Entities;
using qualyteam_api.Infrastructure.Data;
using qualyteam_api.Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Permitir requisições do frontend
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Permite credenciais, se necessário
    });
});

// Registrar repositório e serviço
builder.Services.AddScoped<IIndicatorRepository, IndicatorRepository>();
builder.Services.AddScoped<IIndicatorService, IndicatorService>();

// Configurar DbContext com banco de dados em memória
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("QualyteamDb"));

// Construir o aplicativo
var app = builder.Build();

// Configurar o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Qualyteam API V1");
        c.RoutePrefix = string.Empty; // Acessar Swagger diretamente na raiz (/)
    });
}

// Middleware para redirecionamento HTTPS
app.UseHttpsRedirection();

// Usar CORS antes de mapear os controladores
app.UseCors("AllowFrontend");

// Mapear os controladores
app.MapControllers();

// Forçar o backend a rodar na porta 5000
app.Run("http://localhost:5000");