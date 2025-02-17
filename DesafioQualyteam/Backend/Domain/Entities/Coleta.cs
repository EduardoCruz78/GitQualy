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

        // Para evitar recursão na serialização, ignoramos esta propriedade
        [JsonIgnore]
        public Indicador Indicador { get; init; }

        // Construtor para criação de uma nova coleta
        public Coleta(DateTime data, decimal valor, Indicador indicador)
        {
            if (indicador == null)
                throw new ArgumentNullException(nameof(indicador));

            Data = data;
            Valor = valor;
            Indicador = indicador;
            IndicadorId = indicador.Id;
        }

        // Construtor sem parâmetros para o EF Core
        [SetsRequiredMembers]
        protected Coleta()
        {
            Indicador = null!;
        }

        // Método para atualizar os campos da coleta
        public void Atualizar(DateTime novaData, decimal novoValor)
        {
            Data = novaData;
            Valor = novoValor;
        }
    }
}
