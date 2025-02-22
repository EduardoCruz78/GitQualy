using Backend.Application.Services;
using Backend.Domain.Entities;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Backend.Tests.UnitTests
{
    public class IndicatorServiceTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly IndicatorService _service;

        public IndicatorServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);
            _service = new IndicatorService(_context);
        }

        [Fact]
        public async Task CadastrarIndicadorAsync_Deve_Criar_Indicador()
        {
            string nome = "Indicador Teste";
            var indicador = await _service.CadastrarIndicadorAsync(nome, TipoCalculo.Soma);
            Assert.NotNull(indicador);
            Assert.Equal(nome, indicador.Nome);
            Assert.Equal(TipoCalculo.Soma, indicador.TipoCalculo);
        }

        [Fact]
        public async Task CalcularResultadoAsync_Deve_Retornar_Soma_Quando_TipoCalculo_For_SOMA()
        {
            var indicador = new Indicador("Teste", TipoCalculo.Soma);
            indicador.AdicionarColeta(new Coleta(DateTime.Now, 10, indicador));
            indicador.AdicionarColeta(new Coleta(DateTime.Now, 20, indicador));
            _context.Indicadores.Add(indicador);
            await _context.SaveChangesAsync();

            var resultado = await _service.CalcularResultadoAsync(indicador.Id);
            Assert.Equal(30, resultado);
        }

        [Fact]
        public async Task CalcularResultadoAsync_Deve_Retornar_Media_Quando_TipoCalculo_For_MEDIA()
        {
            var indicador = new Indicador("Teste", TipoCalculo.Media);
            indicador.AdicionarColeta(new Coleta(DateTime.Now, 10, indicador));
            indicador.AdicionarColeta(new Coleta(DateTime.Now, 20, indicador));
            _context.Indicadores.Add(indicador);
            await _context.SaveChangesAsync();

            var resultado = await _service.CalcularResultadoAsync(indicador.Id);
            Assert.Equal(15, resultado);
        }

        [Fact]
        public async Task AtualizarColetaAsync_Deve_Atualizar_Coleta_Corretamente()
        {
            var indicador = new Indicador("Teste Atualizar", TipoCalculo.Soma);
            var coleta = new Coleta(DateTime.Now.AddDays(-1), 50, indicador);
            indicador.AdicionarColeta(coleta);
            _context.Indicadores.Add(indicador);
            await _context.SaveChangesAsync();

            DateTime novaData = DateTime.Now;
            decimal novoValor = 100;
            await _service.AtualizarColetaAsync(coleta.Id, novaData, novoValor);

            var updatedColeta = await _context.Coletas.FindAsync(coleta.Id)
                ?? throw new InvalidOperationException("Coleta não encontrada.");

            Assert.Equal(novaData, updatedColeta.Data);
            Assert.Equal(novoValor, updatedColeta.Valor);
        }

        [Fact]
        public async Task AtualizarColetaAsync_Deve_Lancar_Excecao_Quando_Coleta_Nao_Encontrada()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await _service.AtualizarColetaAsync(999, DateTime.Now, 100);
            });
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
