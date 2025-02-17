using qualyteam_api.Domain.Entities;
using qualyteam_api.Domain.ValueObjects;

namespace qualyteam_api.Application.Interfaces;

public interface IIndicatorService
{
    Task<Indicator> CreateIndicatorAsync(string name, CalculationType calculationType);
    Task AddCollectionAsync(Guid indicatorId, DateTime date, double value);
    Task UpdateCollectionAsync(Guid indicatorId, Guid collectionId, double newValue);
    Task<IEnumerable<Indicator>> GetAllIndicatorsAsync();
    Task<double> CalculateResultAsync(Guid indicatorId);
}