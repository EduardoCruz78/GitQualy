using Microsoft.EntityFrameworkCore;
using qualyteam_api.Application.Interfaces;
using qualyteam_api.Domain.Entities;
using qualyteam_api.Domain.ValueObjects;
using qualyteam_api.Infrastructure.Data;

namespace qualyteam_api.Infrastructure.Repositories;

public class IndicatorRepository : IIndicatorRepository
{
    private readonly ApplicationDbContext _context;

    public IndicatorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Indicator> CreateAsync(Indicator indicator)
    {
        _context.Indicators.Add(indicator);
        await _context.SaveChangesAsync();
        return indicator;
    }

    public async Task<Indicator?> GetByIdAsync(Guid id)
    {
        return await _context.Indicators
            .Include(i => i.Collections)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Indicator>> GetAllAsync()
    {
        return await _context.Indicators
            .Include(i => i.Collections)
            .ToListAsync();
    }

    public async Task AddCollectionAsync(Guid indicatorId, Collection collection)
    {
        var indicator = await _context.Indicators.FindAsync(indicatorId);
        if (indicator != null)
        {
            indicator.Collections.Add(collection);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateCollectionAsync(Guid indicatorId, Guid collectionId, double newValue)
    {
        var collection = await _context.Collections
            .FirstOrDefaultAsync(c => c.IndicatorId == indicatorId && c.Id == collectionId);
        if (collection != null)
        {
            collection.Value = newValue;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<double> CalculateResultAsync(Guid indicatorId)
    {
        var indicator = await _context.Indicators
            .Include(i => i.Collections)
            .FirstOrDefaultAsync(i => i.Id == indicatorId);

        if (indicator == null || !indicator.Collections.Any())
            return 0;

        var values = indicator.Collections.Select(c => c.Value).ToList();

        return indicator.CalculationType switch
        {
            CalculationType.Sum => values.Sum(),
            CalculationType.Average => values.Average(),
            _ => 0
        };
    }
}