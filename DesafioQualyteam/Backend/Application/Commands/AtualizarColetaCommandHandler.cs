using MediatR;
using Backend.Common;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;

namespace Backend.Application.Commands
{
    public class AtualizarColetaCommandHandler : IRequestHandler<AtualizarColetaCommand, Result>
    {
        private readonly ApplicationDbContext _context;

        public AtualizarColetaCommandHandler(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Result> Handle(AtualizarColetaCommand request, CancellationToken cancellationToken)
        {
            var indicador = await _context.Indicadores
                .Include(i => i.Coletas)
                .FirstOrDefaultAsync(i => i.Coletas.Any(c => c.Id == request.ColetaId), cancellationToken);

            if (indicador == null)
                return Result.Failure("Coleta não encontrada.");

            bool atualizado = indicador.AtualizarColeta(request.ColetaId, request.NovaData, request.NovoValor);
            if (!atualizado)
                return Result.Failure("Falha ao atualizar a coleta.");

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}