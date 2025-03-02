using MediatR;
using Backend.Common;
using Backend.Domain.Entities;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Commands
{
    public class RegistrarColetaCommandHandler : IRequestHandler<RegistrarColetaCommand, Result>
    {
        private readonly ApplicationDbContext _context;

        public RegistrarColetaCommandHandler(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Result> Handle(RegistrarColetaCommand request, CancellationToken cancellationToken)
        {
            var indicador = await _context.Indicadores
                .Include(i => i.Coletas)
                .FirstOrDefaultAsync(i => i.Id == request.IndicadorId, cancellationToken);

            if (indicador == null)
                return Result.Failure("Indicador não encontrado.");

            indicador.RegistrarColeta(request.Data, request.Valor);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}