// Arquivo: Backend/WebApi/Program.cs
using Microsoft.AspNetCore.OpenApi;
using Backend.Application.Services;
using Backend.Domain.Entities;
using Backend.WebApi.DTOs;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do EF Core com SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// Registro do serviço
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

// GET /api/indicadores
app.MapGet("/api/indicadores", async (IndicatorService service) =>
{
    var indicadores = await service.GetIndicadoresAsync();
    var response = indicadores.Select(i => new IndicadorResponse(i.Id, i.Nome, i.TipoCalculo.ToString().ToUpper()));
    return Results.Ok(response);
})
.WithName("GetIndicadores");

// GET /api/coletas
app.MapGet("/api/coletas", async (IndicatorService service) =>
{
    var indicadores = await service.GetIndicadoresAsync();
    var coletas = indicadores.SelectMany(i => i.Coletas);
    var response = coletas.Select(c => new ColetaResponse(c.Id, c.Data, c.Valor, c.IndicadorId));
    return Results.Ok(response);
})
.WithName("GetColetas")
.WithOpenApi();

// POST /api/indicadores
// Arquivo: Backend/WebApi/Program.cs
app.MapPost("/api/indicadores", async (IndicadorRequest request, IndicatorService service) =>
{
    if (string.IsNullOrWhiteSpace(request.Nome) || string.IsNullOrWhiteSpace(request.FormaCalculo))
        return Results.BadRequest("Os campos 'Nome' e 'FormaCalculo' são obrigatórios.");

    // Conversão de string para TipoCalculo
    if (!Enum.TryParse<TipoCalculo>(request.FormaCalculo, true, out var tipoCalculo))
        return Results.BadRequest("FormaCalculo inválido.");

    try
    {
        var indicador = await service.CadastrarIndicadorAsync(request.Nome, tipoCalculo);
        var response = new IndicadorResponse(indicador.Id, indicador.Nome, indicador.TipoCalculo.ToString().ToUpper());
        return Results.Created($"/api/indicadores/{indicador.Id}", response);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});


// POST /api/coletas
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

// GET /api/indicadores/{id}/resultado
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

// PUT /api/coletas/{id}
app.MapPut("/api/coletas/{id}", async (int id, ColetaUpdateRequest request, IndicatorService service) =>
{
    try
    {
        await service.AtualizarColetaAsync(id, request.Data, request.Valor);
        return Results.Ok();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithName("AtualizarColeta")
.WithOpenApi();

app.Run();
