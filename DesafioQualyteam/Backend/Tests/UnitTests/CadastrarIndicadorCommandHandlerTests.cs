using Backend.Application.Commands;
using Backend.Domain.Entities;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Backend.Tests.UnitTests
{
    public class CadastrarIndicadorCommandHandlerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsSuccessResultWithIndicador()
        {
            using var context = GetInMemoryDbContext();
            var handler = new CadastrarIndicadorCommandHandler(context);
            var command = new CadastrarIndicadorCommand("Indicador Teste", TipoCalculo.Soma);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal("Indicador Teste", result.Value.Nome);
            Assert.Equal(TipoCalculo.Soma, result.Value.TipoCalculo);
            Assert.Equal(1, await context.Indicadores.CountAsync());
        }
    }
}