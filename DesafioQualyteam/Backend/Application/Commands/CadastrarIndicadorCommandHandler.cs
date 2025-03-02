using MediatR;
using Backend.Common;
using Backend.Domain.Entities;
using Backend.Infrastructure.Data;

namespace Backend.Application.Commands
{
    public class CadastrarIndicadorCommandHandler : IRequestHandler<CadastrarIndicadorCommand, Result<Indicador>>
    {
        private readonly ApplicationDbContext _context;

        public CadastrarIndicadorCommandHandler(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Result<Indicador>> Handle(CadastrarIndicadorCommand request, CancellationToken cancellationToken)
        {
            var indicador = new Indicador(request.Nome, request.TipoCalculo);
            _context.Indicadores.Add(indicador);
            await _context.SaveChangesAsync(cancellationToken);
            return Result<Indicador>.Success(indicador);
        }
    }
}