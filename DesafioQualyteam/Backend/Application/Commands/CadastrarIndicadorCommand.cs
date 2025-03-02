using MediatR;
using Backend.Common;
using Backend.Domain.Entities;

namespace Backend.Application.Commands
{
    public record CadastrarIndicadorCommand(string Nome, TipoCalculo TipoCalculo) : IRequest<Result<Indicador>>;
}