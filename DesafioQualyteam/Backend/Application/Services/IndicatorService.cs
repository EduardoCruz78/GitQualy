using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Application.Services
{
    public class IndicatorService
    {
        private readonly IIndicadorRepository _repository;

        public IndicatorService(IIndicadorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Indicador>> GetIndicadoresAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Indicador> CadastrarIndicadorAsync(string nome, string formaCalculo)
        {
            if (formaCalculo != "MÉDIA" && formaCalculo != "SOMA")
                throw new ArgumentException("Forma de cálculo inválida.", nameof(formaCalculo));

            var indicador = new Indicador(nome, formaCalculo);
            await _repository.AddIndicadorAsync(indicador);
            await _repository.SaveChangesAsync();

            return indicador;
        }

        public async Task RegistrarColetaAsync(int indicadorId, DateTime data, decimal valor)
        {
            var indicador = await _repository.GetByIdAsync(indicadorId);
            if (indicador == null)
                throw new Exception("Indicador não encontrado.");

            var coleta = new Coleta(data, valor, indicador);
            await _repository.AddColetaAsync(coleta);
            await _repository.SaveChangesAsync();
        }

        // NOVO método para atualizar uma coleta
        public async Task AtualizarColetaAsync(int coletaId, DateTime novaData, decimal novoValor)
        {
            var coleta = await _repository.GetColetaByIdAsync(coletaId);
            if (coleta == null)
                throw new Exception("Coleta não encontrada.");

            coleta.Atualizar(novaData, novoValor);
            await _repository.SaveChangesAsync();
        }

        public async Task<decimal> CalcularResultadoAsync(int indicadorId)
        {
            var indicador = await _repository.GetByIdAsync(indicadorId);
            if (indicador == null)
                throw new Exception("Indicador não encontrado.");

            var valores = indicador.Coletas.Select(c => c.Valor);
            return indicador.FormaCalculo switch
            {
                "SOMA" => valores.Sum(),
                "MÉDIA" => valores.Any() ? valores.Average() : 0,
                _ => throw new Exception("Forma de cálculo inválida.")
            };
        }
    }
}
