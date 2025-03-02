using MediatR;
using Backend.Common;

namespace Backend.Application.Queries
{
    public record CalcularResultadoQuery(int IndicadorId) : IRequest<Result<decimal>>;
}