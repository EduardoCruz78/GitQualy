using MediatR;
using Backend.Domain.Entities;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Queries
{
    public class GetIndicadoresQueryHandler : IRequestHandler<GetIndicadoresQuery, List<Indicador>>
    {
        private readonly ApplicationDbContext _context;

        public GetIndicadoresQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Indicador>> Handle(GetIndicadoresQuery request, CancellationToken cancellationToken)
        {
            return await _context.Indicadores.Include(i => i.Coletas).ToListAsync(cancellationToken);
        }
    }
}