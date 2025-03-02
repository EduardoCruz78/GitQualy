using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Domain.Entities
{
    public enum TipoCalculo
    {
        Soma,
        Media
    }

    public class Indicador
    {
        public int Id { get; private set; }
        public string? Nome { get; private set; }
        public TipoCalculo TipoCalculo { get; private set; }
        public List<Coleta> Coletas { get; private set; } = new();

        public Indicador(string nome, TipoCalculo tipoCalculo)
        {
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            TipoCalculo = tipoCalculo;
        }

        protected Indicador() { }

        public void RegistrarColeta(DateTime data, decimal valor)
        {
            var coleta = new Coleta(data, valor, this);
            Coletas.Add(coleta);
        }

        public bool AtualizarColeta(int coletaId, DateTime novaData, decimal novoValor)
        {
            var coleta = Coletas.FirstOrDefault(c => c.Id == coletaId);
            if (coleta == null)
                return false;
            coleta.AtualizarData(novaData);
            coleta.AtualizarValor(novoValor);
            return true;
        }

        public decimal CalcularResultado()
        {
            return TipoCalculo switch
            {
                TipoCalculo.Soma => Coletas.Sum(c => c.Valor),
                TipoCalculo.Media => Coletas.Any() ? Coletas.Average(c => c.Valor) : 0,
                _ => throw new InvalidOperationException("Tipo de cálculo inválido.")
            };
        }
    }
}