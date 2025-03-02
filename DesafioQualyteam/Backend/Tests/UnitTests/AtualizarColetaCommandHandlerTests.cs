using Backend.Application.Commands;
using Backend.Common;
using Backend.Domain.Entities;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Backend.Tests.UnitTests
{
    public class AtualizarColetaCommandHandlerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            using var context = GetInMemoryDbContext();
            var indicador = new Indicador("Teste", TipoCalculo.Soma);
            indicador.RegistrarColeta(DateTime.Now, 50m);
            context.Indicadores.Add(indicador);
            await context.SaveChangesAsync();

            var coletaId = indicador.Coletas.First().Id;
            var newDate = DateTime.Now.AddDays(1);
            var newValue = 75m;

            var handler = new AtualizarColetaCommandHandler(context);
            var command = new AtualizarColetaCommand(coletaId, newDate, newValue);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
            var updatedColeta = await context.Coletas.FindAsync(coletaId);
            Assert.NotNull(updatedColeta);
            Assert.Equal(newDate.Date, updatedColeta.Data.Date);
            Assert.Equal(newValue, updatedColeta.Valor);
        }
    }
}