using System;
using System.Collections.Generic;

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
        public string Nome { get; private set; }
        public TipoCalculo TipoCalculo { get; private set; }
        public List<Coleta> Coletas { get; private set; } = new();

        public Indicador(string nome, TipoCalculo tipoCalculo)
        {
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            TipoCalculo = tipoCalculo;
        }

        // Construtor sem parâmetros para o EF Core
        protected Indicador()
        {
            Nome = string.Empty;
        }

        public void AdicionarColeta(Coleta coleta)
        {
            Coletas.Add(coleta ?? throw new ArgumentNullException(nameof(coleta)));
        }
    }
}
