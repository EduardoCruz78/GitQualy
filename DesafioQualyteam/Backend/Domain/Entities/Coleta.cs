using System;

namespace Backend.Domain.Entities;

public class Coleta
{
    public int Id { get; set; }

    public required DateTime Data { get; set; } // Propriedade obrigatória
    public required decimal Valor { get; set; } // Propriedade obrigatória
    public required int IndicadorId { get; set; } // Propriedade obrigatória
    public required Indicador Indicador { get; set; } // Propriedade obrigatória

    // Construtor vazio necessário para o EF Core
    public Coleta()
    {
    }

    // Método auxiliar para configurar os valores
    public void Configurar(DateTime data, decimal valor, Indicador indicador)
    {
        Data = data;
        Valor = valor;
        Indicador = indicador;
        IndicadorId = indicador.Id; // Associa o ID do indicador automaticamente
    }
}