using System.Collections.Generic;

namespace Backend.Domain.Entities;

public class Indicador
{
    public int Id { get; set; }

    // Propriedades obrigatórias
    public required string Nome { get; set; }
    public required string FormaCalculo { get; set; }

    // Propriedade de navegação
    public ICollection<Coleta> Coletas { get; set; } = new List<Coleta>(); // Inicialização padrão

    // Construtor para inicializar as propriedades obrigatórias
    public Indicador(string nome, string formaCalculo)
    {
        if (string.IsNullOrWhiteSpace(nome)) 
            throw new ArgumentException("O nome do indicador é obrigatório.");
        
        if (string.IsNullOrWhiteSpace(formaCalculo)) 
            throw new ArgumentException("A forma de cálculo do indicador é obrigatória.");

        Nome = nome;
        FormaCalculo = formaCalculo;
    }

    // Construtor vazio necessário para o EF Core
    public Indicador()
    {
    }
}
