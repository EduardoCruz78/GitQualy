using Backend;
using Backend.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Backend.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Configura o Entity Framework Core com SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// Adiciona o serviço de CORS
builder.Services.AddCors();

// Registra o serviço IndicadorService no contêiner de injeção de dependência
builder.Services.AddScoped<IndicadorService>();

// Adiciona serviços necessários para controladores e endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Desafio Qualyteam", Version = "v1" });
});

// Configura o pipeline de requisições
var app = builder.Build();

// Configura o CORS
app.UseCors(policy =>
{
    policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

// Middleware para detalhar exceções no ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // Middleware para redirecionamento HTTPS em produção
    app.UseHttpsRedirection();
}

// Configura o Swagger
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio Qualyteam v1"));

// Configura os endpoints
app.MapGet("/api/Indicador", async (IndicadorService service) =>
{
    var indicadores = await service.GetIndicadoresAsync();
    return Results.Ok(indicadores);
})
.WithName("GetIndicadores")
.WithOpenApi();

app.MapPost("/api/Indicador/cadastrar", async (IndicadorRequest request, IndicadorService service) =>
{
    if (string.IsNullOrEmpty(request.Nome) || string.IsNullOrEmpty(request.FormaCalculo))
    {
        return Results.BadRequest("Os campos 'Nome' e 'FormaCalculo' são obrigatórios.");
    }

    if (request.FormaCalculo != "MÉDIA" && request.FormaCalculo != "SOMA")
    {
        return Results.BadRequest("O campo 'FormaCalculo' deve ser 'MÉDIA' ou 'SOMA'.");
    }

    var indicador = await service.CadastrarIndicadorAsync(request.Nome, request.FormaCalculo);
    return Results.Created($"/api/Indicador/{indicador.Id}", indicador);
})
.WithName("CadastrarIndicador")
.WithOpenApi();

app.MapPost("/api/Indicador/coleta", async (ColetaRequest request, IndicadorService service) =>
{
    if (request.IndicadorId <= 0 || request.Valor < 0)
    {
        return Results.BadRequest("Os campos 'IndicadorId' e 'Valor' devem ser válidos.");
    }

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
.WithName("RegistrarColeta")
.WithOpenApi();

app.MapGet("/api/Indicador/resultado/{id}", async (int id, IndicadorService service) =>
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
.WithName("CalcularResultado")
.WithOpenApi();

// Inicia o aplicativo
app.Run();

// DTOs (Objetos de Transferência de Dados)
public record IndicadorRequest(string Nome, string FormaCalculo);
public record ColetaRequest(int IndicadorId, DateTime Data, decimal Valor);
