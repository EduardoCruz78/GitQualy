using MediatR;
using Backend.Common;

namespace Backend.Application.Commands
{
    public record AtualizarColetaCommand(int ColetaId, DateTime NovaData, decimal NovoValor) : IRequest<Result>;
}