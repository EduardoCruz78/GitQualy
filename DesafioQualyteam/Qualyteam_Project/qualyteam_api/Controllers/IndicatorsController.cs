using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using qualyteam_api.Application.Interfaces;
using qualyteam_api.Domain.Entities;
using qualyteam_api.Domain.ValueObjects;

namespace qualyteam_api.Controllers
{
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
        public async Task<IActionResult> CreateIndicator([FromBody] IndicatorRequest request)
        {
            if (request == null)
                return BadRequest("Dados inválidos.");

            var indicator = await _service.CreateIndicatorAsync(request.Name, request.CalculationType);
            return CreatedAtAction(nameof(GetIndicatorById), new { id = indicator.Id }, indicator);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetIndicatorById(Guid id)
        {
            var indicator = await _service.GetIndicatorByIdAsync(id);
            if (indicator == null) return NotFound();
            return Ok(indicator);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIndicators()
        {
            var indicators = await _service.GetAllIndicatorsAsync();
            return Ok(indicators);
        }

        [HttpPost("{id:guid}/collections")]
        public async Task<IActionResult> AddCollection(Guid id, [FromBody] CollectionRequest request)
        {
            await _service.AddCollectionAsync(id, request.Date, request.Value);
            return NoContent();
        }

        [HttpGet("{id:guid}/result")]
        public async Task<IActionResult> CalculateResult(Guid id)
        {
            var result = await _service.CalculateResultAsync(id);
            return Ok(result);
        }
    }

    public class IndicatorRequest
    {
        public string Name { get; set; } = string.Empty;
        public CalculationType CalculationType { get; set; }
    }

    public class CollectionRequest
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}