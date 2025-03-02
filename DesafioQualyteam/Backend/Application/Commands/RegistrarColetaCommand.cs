using MediatR;
using Backend.Common;

namespace Backend.Application.Commands
{
    public record RegistrarColetaCommand(int IndicadorId, DateTime Data, decimal Valor) : IRequest<Result>;
}