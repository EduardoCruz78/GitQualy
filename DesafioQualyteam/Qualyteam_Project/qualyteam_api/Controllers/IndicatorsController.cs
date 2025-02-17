using Microsoft.AspNetCore.Mvc;
using qualyteam_api.Application.Interfaces;
using qualyteam_api.Domain.Entities;
using qualyteam_api.Domain.ValueObjects;


namespace qualyteam_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IndicatorsController : ControllerBase
{
    private readonly IIndicatorService _service;

    public IndicatorsController(IIndicatorService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateIndicator([FromBody] CreateIndicatorRequest request)
    {
        var indicator = await _service.CreateIndicatorAsync(request.Name, request.CalculationType);
        return CreatedAtAction(nameof(GetIndicator), new { id = indicator.Id }, indicator);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetIndicator(Guid id)
    {
        var indicator = await _service.GetAllIndicatorsAsync();
        return Ok(indicator);
    }

    [HttpPost("{id}/collections")]
    public async Task<IActionResult> AddCollection(Guid id, [FromBody] AddCollectionRequest request)
    {
        await _service.AddCollectionAsync(id, request.Date, request.Value);
        return NoContent();
    }

    [HttpPut("{id}/collections/{collectionId}")]
    public async Task<IActionResult> UpdateCollection(Guid id, Guid collectionId, [FromBody] UpdateCollectionRequest request)
    {
        await _service.UpdateCollectionAsync(id, collectionId, request.NewValue);
        return NoContent();
    }

    [HttpGet("{id}/result")]
    public async Task<IActionResult> CalculateResult(Guid id)
    {
        var result = await _service.CalculateResultAsync(id);
        return Ok(result);
    }
}

public record CreateIndicatorRequest(string Name, CalculationType CalculationType);
public record AddCollectionRequest(DateTime Date, double Value);
public record UpdateCollectionRequest(double NewValue);