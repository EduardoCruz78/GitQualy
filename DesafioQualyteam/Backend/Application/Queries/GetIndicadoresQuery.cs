using MediatR;
using Backend.Domain.Entities;

namespace Backend.Application.Queries
{
    public record GetIndicadoresQuery : IRequest<List<Indicador>>;
}