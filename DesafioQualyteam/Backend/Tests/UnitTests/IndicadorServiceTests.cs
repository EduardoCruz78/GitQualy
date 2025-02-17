using Backend.Application.Services;
using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Backend.Tests.UnitTests
{
    public class IndicadorServiceTests
    {
        private readonly Mock<IIndicadorRepository> _mockRepo;
        private readonly IndicatorService _service;

        public IndicadorServiceTests()
        {
            _mockRepo = new Mock<IIndicadorRepository>();
            _service = new IndicatorService(_mockRepo.Object);
        }

        [Fact]
        public async Task CadastrarIndicadorAsync_Deve_Criar_Indicador()
        {
            // Arrange
            string nome = "Indicador Teste";
            string formaCalculo = "SOMA";
            _mockRepo.Setup(r => r.AddIndicadorAsync(It.IsAny<Indicador>())).Returns(Task.CompletedTask);
            _mockRepo.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var indicador = await _service.CadastrarIndicadorAsync(nome, formaCalculo);

            // Assert
            Assert.NotNull(indicador);
            Assert.Equal(nome, indicador.Nome);
            Assert.Equal(formaCalculo, indicador.FormaCalculo);
        }

        [Fact]
        public async Task CalcularResultadoAsync_Deve_Retornar_Soma_Quando_FormaCalculo_For_SOMA()
        {
            // Arrange
            var indicador = new Indicador("Teste", "SOMA");
            var coleta1 = new Coleta(DateTime.Now, 10, indicador);
            var coleta2 = new Coleta(DateTime.Now, 20, indicador);
            indicador.AddColeta(coleta1);
            indicador.AddColeta(coleta2);

            _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(indicador);

            // Act
            var resultado = await _service.CalcularResultadoAsync(1);

            // Assert
            Assert.Equal(30, resultado);
        }

        [Fact]
        public async Task CalcularResultadoAsync_Deve_Retornar_Media_Quando_FormaCalculo_For_MEDIA()
        {
            // Arrange
            var indicador = new Indicador("Teste", "MÉDIA");
            var coleta1 = new Coleta(DateTime.Now, 10, indicador);
            var coleta2 = new Coleta(DateTime.Now, 20, indicador);
            indicador.AddColeta(coleta1);
            indicador.AddColeta(coleta2);

            _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(indicador);

            // Act
            var resultado = await _service.CalcularResultadoAsync(1);

            // Assert
            Assert.Equal(15, resultado);
        }

        [Fact]
        public async Task AtualizarColetaAsync_Deve_Atualizar_Coleta_Corretamente()
        {
            // Arrange
            var indicador = new Indicador("Teste Atualizar", "SOMA");
            var coleta = new Coleta(DateTime.Now.AddDays(-1), 50, indicador);
            indicador.AddColeta(coleta);

            _mockRepo.Setup(r => r.GetColetaByIdAsync(It.IsAny<int>())).ReturnsAsync(coleta);
            _mockRepo.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            DateTime novaData = DateTime.Now;
            decimal novoValor = 100;

            // Act
            await _service.AtualizarColetaAsync(coleta.Id, novaData, novoValor);

            // Assert
            Assert.Equal(novaData, coleta.Data);
            Assert.Equal(novoValor, coleta.Valor);
        }

        [Fact]
        public async Task AtualizarColetaAsync_Deve_Lancar_Excecao_Quando_Coleta_Nao_Encontrada()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetColetaByIdAsync(It.IsAny<int>())).ReturnsAsync((Coleta?)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _service.AtualizarColetaAsync(999, DateTime.Now, 100);
            });
        }
    }
}
