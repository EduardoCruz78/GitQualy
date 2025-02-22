using Backend.Domain.Entities;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Application.Services
{
    public class IndicatorService
    {
        private readonly ApplicationDbContext _context;

        public IndicatorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Indicador> CadastrarIndicadorAsync(string nome, TipoCalculo tipoCalculo)
        {
            var indicador = new Indicador(nome, tipoCalculo);
            _context.Indicadores.Add(indicador);
            await _context.SaveChangesAsync();
            return indicador;
        }

        public async Task<decimal> CalcularResultadoAsync(int indicadorId)
        {
            var indicador = await _context.Indicadores
                .Include(i => i.Coletas)
                .FirstOrDefaultAsync(i => i.Id == indicadorId)
                ?? throw new InvalidOperationException("Indicador não encontrado.");

            return indicador.TipoCalculo switch
            {
                TipoCalculo.Soma => indicador.Coletas.Sum(c => c.Valor),
                TipoCalculo.Media => indicador.Coletas.Average(c => c.Valor),
                _ => throw new InvalidOperationException("Tipo de cálculo inválido.")
            };
        }

        public async Task RegistrarColetaAsync(int indicadorId, DateTime data, decimal valor)
        {
            var indicador = await _context.Indicadores.FindAsync(indicadorId)
                ?? throw new InvalidOperationException("Indicador não encontrado.");

            var coleta = new Coleta(data, valor, indicador);
            _context.Coletas.Add(coleta);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarColetaAsync(int coletaId, DateTime novaData, decimal novoValor)
        {
            var coleta = await _context.Coletas.FindAsync(coletaId)
                ?? throw new InvalidOperationException("Coleta não encontrada.");

            coleta.AtualizarData(novaData);
            coleta.AtualizarValor(novoValor);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Indicador>> GetIndicadoresAsync()
        {
            return await _context.Indicadores.Include(i => i.Coletas).ToListAsync();
        }
    }
}
