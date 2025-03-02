using Backend.Application.Queries;
using Backend.Domain.Entities;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Backend.Tests.UnitTests
{
    public class GetIndicadoresQueryHandlerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Handle_RetornaListaDeIndicadores()
        {
            using var context = GetInMemoryDbContext();
            context.Indicadores.Add(new Indicador("Teste", TipoCalculo.Soma));
            await context.SaveChangesAsync();

            var handler = new GetIndicadoresQueryHandler(context);
            var query = new GetIndicadoresQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Single(result);
            Assert.Equal("Teste", result[0].Nome);
        }
    }
}