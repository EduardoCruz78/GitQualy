using MediatR;
using Backend.Common;
using Backend.Domain.Entities;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Queries
{
    public class CalcularResultadoQueryHandler : IRequestHandler<CalcularResultadoQuery, Result<decimal>>
    {
        private readonly ApplicationDbContext _context;

        public CalcularResultadoQueryHandler(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Result<decimal>> Handle(CalcularResultadoQuery request, CancellationToken cancellationToken)
        {
            var indicador = await _context.Indicadores
                .Include(i => i.Coletas)
                .FirstOrDefaultAsync(i => i.Id == request.IndicadorId, cancellationToken);

            if (indicador == null)
                return Result<decimal>.FailWithError("Indicador não encontrado.");

            decimal resultado = indicador.CalcularResultado();
            return Result<decimal>.Success(resultado);
        }
    }
}