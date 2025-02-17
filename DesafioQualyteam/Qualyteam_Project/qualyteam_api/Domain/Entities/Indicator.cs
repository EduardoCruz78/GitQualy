using System;
using System.Collections.Generic;
using System.Linq;
using qualyteam_api.Domain.ValueObjects;

namespace qualyteam_api.Domain.Entities
{
    public class Indicator
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public CalculationType CalculationType { get; private set; }
        public ICollection<Collection> Collections { get; private set; } = new List<Collection>();

        public Indicator(string name, CalculationType calculationType)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name), "O nome do indicador não pode ser nulo.");
            CalculationType = calculationType;
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("O novo nome não pode ser nulo ou vazio.", nameof(newName));
            Name = newName;
        }

        public void UpdateCalculationType(CalculationType newCalculationType)
        {
            CalculationType = newCalculationType;
        }

        public void AddCollection(DateTime date, decimal value)
        {
            Collections.Add(new Collection(date, value));
        }

        public void UpdateCollection(DateTime date, decimal newValue)
        {
            var collection = Collections.FirstOrDefault(c => c.Date == date);
            if (collection == null)
                throw new KeyNotFoundException("Coleta não encontrada para a data especificada.");
            collection.Value = newValue;
        }

        public decimal CalculateResult()
        {
            if (!Collections.Any()) return 0;
            return CalculationType switch
            {
                CalculationType.Soma => Collections.Sum(c => c.Value),
                CalculationType.Media => Collections.Average(c => c.Value),
                _ => throw new InvalidOperationException("Tipo de cálculo inválido.")
            };
        }
    }
}