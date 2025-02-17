// Arquivo: Backend/Application/Services/IndicatorService.cs

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

            // Cria o objeto usando o construtor que agora define os required members.
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
