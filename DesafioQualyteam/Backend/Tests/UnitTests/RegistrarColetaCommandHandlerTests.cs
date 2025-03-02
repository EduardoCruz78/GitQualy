using Backend.Application.Commands;
using Backend.Common;
using Backend.Domain.Entities;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Backend.Tests.UnitTests
{
    public class RegistrarColetaCommandHandlerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Handle_IndicadorExistente_RegistraColeta()
        {
            using var context = GetInMemoryDbContext();
            var indicador = new Indicador("Teste", TipoCalculo.Soma);
            context.Indicadores.Add(indicador);
            await context.SaveChangesAsync();

            var handler = new RegistrarColetaCommandHandler(context);
            var command = new RegistrarColetaCommand(indicador.Id, DateTime.Now, 100m);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Single(indicador.Coletas);
        }
    }
}