using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using qualyteam_api.Application.Interfaces;
using qualyteam_api.Domain.Entities;
using qualyteam_api.Domain.ValueObjects;

namespace qualyteam_api.Application.Services
{
    public class IndicatorService : IIndicatorService
    {
        private readonly IIndicatorRepository _indicatorRepository;

        public IndicatorService(IIndicatorRepository indicatorRepository)
        {
            _indicatorRepository = indicatorRepository ?? throw new ArgumentNullException(nameof(indicatorRepository));
        }

        public Task<Indicator> CreateIndicatorAsync(string name, CalculationType calculationType)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome do indicador não pode ser nulo ou vazio.", nameof(name));

            var indicator = new Indicator(name, calculationType);
            _indicatorRepository.Add(indicator);
            return Task.FromResult(indicator);
        }

        public Task<Indicator> GetIndicatorByIdAsync(Guid id)
        {
            var indicator = _indicatorRepository.GetById(id);
            if (indicator == null)
                throw new KeyNotFoundException("Indicador não encontrado.");

            return Task.FromResult(indicator);
        }

        public Task<IEnumerable<Indicator>> GetAllIndicatorsAsync()
        {
            return Task.FromResult(_indicatorRepository.GetAll());
        }

        public Task AddCollectionAsync(Guid id, DateTime date, decimal value)
        {
            var indicator = _indicatorRepository.GetById(id);
            if (indicator == null)
                throw new KeyNotFoundException("Indicador não encontrado.");

            indicator.AddCollection(date, value);
            _indicatorRepository.Update(indicator);
            return Task.CompletedTask;
        }

        public Task<decimal> CalculateResultAsync(Guid id)
        {
            var indicator = _indicatorRepository.GetById(id);
            if (indicator == null)
                throw new KeyNotFoundException("Indicador não encontrado.");

            return Task.FromResult(indicator.CalculateResult());
        }
    }
}