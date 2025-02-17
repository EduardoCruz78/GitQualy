// Arquivo: Backend/Domain/Entities/Indicador.cs

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Backend.Domain.Entities
{
    public class Indicador
    {
        public int Id { get; private set; }

        // Propriedades required com init público
        public required string Nome { get; init; }
        public required string FormaCalculo { get; init; }

        public ICollection<Coleta> Coletas { get; private set; } = new List<Coleta>();

        // Construtor que define os required members.
        [SetsRequiredMembers]
        public Indicador(string nome, string formaCalculo)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do indicador é obrigatório.", nameof(nome));
            if (string.IsNullOrWhiteSpace(formaCalculo))
                throw new ArgumentException("A forma de cálculo do indicador é obrigatória.", nameof(formaCalculo));

            Nome = nome;
            FormaCalculo = formaCalculo;
        }

        // Construtor sem parâmetros para o EF Core.
        [SetsRequiredMembers]
        protected Indicador()
        {
            // Atribuindo valores padrão para satisfazer os required members
            Nome = string.Empty;
            FormaCalculo = string.Empty;
            Coletas = new List<Coleta>();
        }

        // Método de domínio para adicionar uma coleta
        public void AddColeta(Coleta coleta)
        {
            if (coleta == null)
                throw new ArgumentNullException(nameof(coleta));

            Coletas.Add(coleta);
        }
    }
}
