using Backend.Application.Queries;
using Backend.Common;
using Backend.Domain.Entities;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Backend.Tests.UnitTests
{
    public class CalcularResultadoQueryHandlerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Handle_SomaWithColetas_ReturnsCorrectSum()
        {
            using var context = GetInMemoryDbContext();
            var indicador = new Indicador("Teste Soma", TipoCalculo.Soma);
            indicador.RegistrarColeta(DateTime.Now, 10m);
            indicador.RegistrarColeta(DateTime.Now, 20m);
            context.Indicadores.Add(indicador);
            await context.SaveChangesAsync();

            var handler = new CalcularResultadoQueryHandler(context);
            var query = new CalcularResultadoQuery(indicador.Id);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(30m, result.Value);
        }

        [Fact]
        public async Task Handle_MediaWithColetas_ReturnsCorrectAverage()
        {
            using var context = GetInMemoryDbContext();
            var indicador = new Indicador("Teste Media", TipoCalculo.Media);
            indicador.RegistrarColeta(DateTime.Now, 10m);
            indicador.RegistrarColeta(DateTime.Now, 20m);
            context.Indicadores.Add(indicador);
            await context.SaveChangesAsync();

            var handler = new CalcularResultadoQueryHandler(context);
            var query = new CalcularResultadoQuery(indicador.Id);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(15m, result.Value);
        }

        [Fact]
        public async Task Handle_IndicadorNaoEncontrado_ReturnsFailure()
        {
            using var context = GetInMemoryDbContext();
            var handler = new CalcularResultadoQueryHandler(context);
            var query = new CalcularResultadoQuery(999); 

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal("Indicador não encontrado.", result.Error);
        }
    }
}