using System;

namespace Backend.Domain.Entities
{
    public class Coleta
    {
        public int Id { get; private set; }
        public DateTime Data { get; private set; }
        public decimal Valor { get; private set; }
        public int IndicadorId { get; private set; }
        public Indicador Indicador { get; private set; } = null!;

        public Coleta(DateTime data, decimal valor, Indicador indicador)
        {
            Data = data;
            Valor = valor;
            Indicador = indicador ?? throw new ArgumentNullException(nameof(indicador));
        }

        protected Coleta() { }

        public void AtualizarData(DateTime novaData)
        {
            Data = novaData;
        }

        public void AtualizarValor(decimal novoValor)
        {
            Valor = novoValor;
        }
    }
}