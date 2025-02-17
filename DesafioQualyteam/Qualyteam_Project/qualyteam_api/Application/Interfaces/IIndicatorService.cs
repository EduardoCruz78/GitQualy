using System.Collections.Generic;
using System.Threading.Tasks;
using qualyteam_api.Domain.Entities;
using qualyteam_api.Domain.ValueObjects;

namespace qualyteam_api.Application.Interfaces
{
    public interface IIndicatorService
    {
        Task<Indicator> CreateIndicatorAsync(string name, CalculationType calculationType);
        Task<Indicator> GetIndicatorByIdAsync(Guid id);
        Task<IEnumerable<Indicator>> GetAllIndicatorsAsync();
        Task AddCollectionAsync(Guid id, DateTime date, decimal value);
        Task<decimal> CalculateResultAsync(Guid id);
    }
}