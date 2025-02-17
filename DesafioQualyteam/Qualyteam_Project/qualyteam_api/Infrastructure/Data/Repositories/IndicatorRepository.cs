using System;
using System.Collections.Generic;
using System.Linq;
using qualyteam_api.Domain.Entities;
using qualyteam_api.Application.Interfaces;

namespace qualyteam_api.Infrastructure.Data.Repositories
{
    public class IndicatorRepository : IIndicatorRepository
    {
        private readonly List<Indicator> _indicators = new();

        public Indicator GetById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("O ID não pode ser vazio.", nameof(id));

            var indicator = _indicators.FirstOrDefault(i => i.Id == id);
            if (indicator == null)
                throw new KeyNotFoundException("Indicador não encontrado.");

            return indicator;
        }

        public void Add(Indicator indicator)
        {
            if (indicator == null)
                throw new ArgumentNullException(nameof(indicator), "O indicador não pode ser nulo.");

            _indicators.Add(indicator);
        }

        public void Update(Indicator indicator)
        {
            if (indicator == null)
                throw new ArgumentNullException(nameof(indicator), "O indicador não pode ser nulo.");

            var existingIndicator = _indicators.FirstOrDefault(i => i.Id == indicator.Id);
            if (existingIndicator != null)
            {
                existingIndicator.UpdateName(indicator.Name);
                existingIndicator.UpdateCalculationType(indicator.CalculationType);
            }
            else
            {
                throw new KeyNotFoundException("Indicador não encontrado.");
            }
        }

        public void Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("O ID não pode ser vazio.", nameof(id));

            var indicator = _indicators.FirstOrDefault(i => i.Id == id);
            if (indicator != null)
            {
                _indicators.Remove(indicator);
            }
            else
            {
                throw new KeyNotFoundException("Indicador não encontrado.");
            }
        }

        public IEnumerable<Indicator> GetAll()
        {
            return _indicators.ToList();
        }
    }
}