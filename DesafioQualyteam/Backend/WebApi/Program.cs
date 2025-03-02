using MediatR;
using Backend.Application.Commands;
using Backend.Application.Queries;
using Backend.Infrastructure.Data;
using Backend.Domain.Entities;
using Backend.WebApi.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

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

app.MapGet("/api/indicadores", async (IMediator mediator) =>
{
    var indicadores = await mediator.Send(new GetIndicadoresQuery());
    var response = indicadores.Select(i => new IndicadorResponse(i.Id, i.Nome ?? "Sem Nome", i.TipoCalculo.ToString().ToUpper()));
    return Results.Ok(response);
})
.WithName("GetIndicadores");

app.MapGet("/api/coletas", async (IMediator mediator) =>
{
    var indicadores = await mediator.Send(new GetIndicadoresQuery());
    var coletas = indicadores.SelectMany(i => i.Coletas);
    var response = coletas.Select(c => new ColetaResponse(c.Id, c.Data, c.Valor, c.IndicadorId));
    return Results.Ok(response);
})
.WithName("GetColetas");

app.MapPost("/api/indicadores", async (IndicadorRequest request, IMediator mediator) =>
{
    if (string.IsNullOrWhiteSpace(request.Nome) || string.IsNullOrWhiteSpace(request.FormaCalculo))
        return Results.BadRequest("Os campos 'Nome' e 'FormaCalculo' são obrigatórios.");

    if (!Enum.TryParse<TipoCalculo>(request.FormaCalculo, true, out var tipoCalculo))
        return Results.BadRequest("FormaCalculo inválido.");

    var result = await mediator.Send(new CadastrarIndicadorCommand(request.Nome, tipoCalculo));
    if (!result.IsSuccess)
        return Results.BadRequest(result.Error);

    var indicador = result.Value!;
    var response = new IndicadorResponse(indicador.Id, indicador.Nome ?? "Sem Nome", indicador.TipoCalculo.ToString().ToUpper());
    return Results.Created($"/api/indicadores/{indicador.Id}", response);
})
.WithName("CadastrarIndicador");

app.MapPost("/api/coletas", async (ColetaRequest request, IMediator mediator) =>
{
    if (request.IndicadorId <= 0 || request.Valor < 0)
        return Results.BadRequest("Os campos 'IndicadorId' e 'Valor' devem ser válidos.");

    var result = await mediator.Send(new RegistrarColetaCommand(request.IndicadorId, request.Data, request.Valor));
    if (!result.IsSuccess)
        return Results.BadRequest(result.Error);
    return Results.Ok();
})
.WithName("RegistrarColeta");

app.MapGet("/api/indicadores/{id}/resultado", async (int id, IMediator mediator) =>
{
    var result = await mediator.Send(new CalcularResultadoQuery(id));
    return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Error);
})
.WithName("CalcularResultado");

app.MapPut("/api/coletas/{id}", async (int id, ColetaUpdateRequest request, IMediator mediator) =>
{
    var command = new AtualizarColetaCommand(id, request.Data, request.Valor);
    var result = await mediator.Send(command);
    if (!result.IsSuccess)
        return Results.BadRequest(result.Error);
    return Results.Ok();
})
.WithName("AtualizarColeta");

app.Run();

public partial class Program { }