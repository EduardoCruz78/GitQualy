using qualyteam_api.Application.Interfaces;
using qualyteam_api.Domain.Entities;
using qualyteam_api.Domain.ValueObjects;

namespace qualyteam_api.Application.Services;

public class IndicatorService : IIndicatorService
{
    private readonly IIndicatorRepository _repository;

    public IndicatorService(IIndicatorRepository repository)
    {
        _repository = repository;
    }

    public async Task<Indicator> CreateIndicatorAsync(string name, CalculationType calculationType)
    {
        var indicator = new Indicator
        {
            Id = Guid.NewGuid(),
            Name = name,
            CalculationType = calculationType
        };
        return await _repository.CreateAsync(indicator);
    }

    public async Task AddCollectionAsync(Guid indicatorId, DateTime date, double value)
    {
        var collection = new Collection
        {
            Id = Guid.NewGuid(),
            Date = date,
            Value = value,
            IndicatorId = indicatorId
        };
        await _repository.AddCollectionAsync(indicatorId, collection);
    }

    public async Task UpdateCollectionAsync(Guid indicatorId, Guid collectionId, double newValue)
    {
        await _repository.UpdateCollectionAsync(indicatorId, collectionId, newValue);
    }

    public async Task<IEnumerable<Indicator>> GetAllIndicatorsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<double> CalculateResultAsync(Guid indicatorId)
    {
        return await _repository.CalculateResultAsync(indicatorId);
    }
}