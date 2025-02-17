using qualyteam_api.Domain.Entities;

namespace qualyteam_api.Application.Interfaces;

public interface IIndicatorRepository
{
    Task<Indicator> CreateAsync(Indicator indicator);
    Task<Indicator?> GetByIdAsync(Guid id);
    Task<IEnumerable<Indicator>> GetAllAsync();
    Task AddCollectionAsync(Guid indicatorId, Collection collection);
    Task UpdateCollectionAsync(Guid indicatorId, Guid collectionId, double newValue);
    Task<double> CalculateResultAsync(Guid indicatorId);
}