using Microsoft.EntityFrameworkCore;
using qualyteam_api.Application.Interfaces;
using qualyteam_api.Application.Services;
using qualyteam_api.Infrastructure.Data;
using qualyteam_api.Infrastructure.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // Suporte para serialização de enums como strings
    });

// Configuração do banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração do repositório e serviço
builder.Services.AddScoped<IIndicatorRepository, IndicatorRepository>();
builder.Services.AddScoped<IIndicatorService, IndicatorService>();

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Permitir requisições do frontend
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Habilitar CORS
app.UseCors("AllowFrontend");

// Desabilitar redirecionamento HTTPS (opcional, se não for necessário)
// app.UseHttpsRedirection();

// Mapear os controladores
app.MapControllers();

// Forçar o backend a rodar na porta 5001
app.Run("http://localhost:5001");

// Health check endpoint (opcional, mas útil para monitoramento)
app.MapGet("/health", () => Results.Ok("Healthy"));

await app.WaitForShutdownAsync();