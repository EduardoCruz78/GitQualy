// Arquivo: Backend/Infrastructure/Repositories/IndicadorRepository.cs

using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Repositories
{
    public class IndicadorRepository : IIndicadorRepository
    {
        private readonly ApplicationDbContext _context;

        public IndicadorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Indicador>> GetAllAsync()
        {
            return await _context.Indicadores.Include(i => i.Coletas).ToListAsync();
        }

        public async Task<Indicador?> GetByIdAsync(int id)
        {
            return await _context.Indicadores
                .Include(i => i.Coletas)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task AddIndicadorAsync(Indicador indicador)
        {
            await _context.Indicadores.AddAsync(indicador);
        }

        public async Task AddColetaAsync(Coleta coleta)
        {
            await _context.Coletas.AddAsync(coleta);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
