// Arquivo: Backend/WebApi/Program.cs

using Backend.Application.Services;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Data;
using Backend.Infrastructure.Repositories;
using Backend.WebApi.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do EF Core com SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// Registro dos repositórios e serviços para injeção de dependência
builder.Services.AddScoped<IIndicadorRepository, IndicadorRepository>();
builder.Services.AddScoped<IndicatorService>();

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Configuração do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Desafio Qualyteam", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio Qualyteam v1"));
}

app.UseCors("AllowAll");

// Endpoints da API RESTful

app.MapGet("/api/indicadores", async (IndicatorService service) =>
{
    var indicadores = await service.GetIndicadoresAsync();
    return Results.Ok(indicadores);
})
.WithName("GetIndicadores");

app.MapPost("/api/indicadores", async (IndicadorRequest request, IndicatorService service) =>
{
    if (string.IsNullOrWhiteSpace(request.Nome) || string.IsNullOrWhiteSpace(request.FormaCalculo))
        return Results.BadRequest("Os campos 'Nome' e 'FormaCalculo' são obrigatórios.");

    try
    {
        var indicador = await service.CadastrarIndicadorAsync(request.Nome, request.FormaCalculo);
        return Results.Created($"/api/indicadores/{indicador.Id}", indicador);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithName("CadastrarIndicador");

app.MapPost("/api/coletas", async (ColetaRequest request, IndicatorService service) =>
{
    if (request.IndicadorId <= 0 || request.Valor < 0)
        return Results.BadRequest("Os campos 'IndicadorId' e 'Valor' devem ser válidos.");

    try
    {
        await service.RegistrarColetaAsync(request.IndicadorId, request.Data, request.Valor);
        return Results.Ok();
    }
    catch (Exception ex)
    {
        return Results.NotFound(ex.Message);
    }
})
.WithName("RegistrarColeta");

app.MapGet("/api/indicadores/{id}/resultado", async (int id, IndicatorService service) =>
{
    try
    {
        var resultado = await service.CalcularResultadoAsync(id);
        return Results.Ok(resultado);
    }
    catch (Exception ex)
    {
        return Results.NotFound(ex.Message);
    }
})
.WithName("CalcularResultado");

app.Run();
