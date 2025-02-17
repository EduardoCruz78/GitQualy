using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Backend.Domain.Entities
{
    public class Coleta
    {
        public int Id { get; private set; }
        public DateTime Data { get; private set; }
        public decimal Valor { get; private set; }
        public int IndicadorId { get; private set; }

        // Ao ignorar essa propriedade na serialização, evitamos a recursão infinita.
        [JsonIgnore]
        public Indicador Indicador { get; init; }

        // Construtor para criar uma nova coleta, garantindo que a propriedade Indicador seja definida.
        public Coleta(DateTime data, decimal valor, Indicador indicador)
        {
            if (indicador == null)
                throw new ArgumentNullException(nameof(indicador));

            Data = data;
            Valor = valor;
            Indicador = indicador;
            IndicadorId = indicador.Id;
        }

        // Construtor sem parâmetros necessário para o EF Core.
        [SetsRequiredMembers]
        protected Coleta()
        {
            // Indicador será definido posteriormente pelo EF Core.
            Indicador = null!;
        }
    }
}
