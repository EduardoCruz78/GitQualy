using Backend.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Domain.Interfaces
{
    public interface IIndicadorRepository
    {
        Task<IEnumerable<Indicador>> GetAllAsync();
        Task<Indicador?> GetByIdAsync(int id);
        Task AddIndicadorAsync(Indicador indicador);
        Task AddColetaAsync(Coleta coleta);
        Task<Coleta?> GetColetaByIdAsync(int id); // NOVO método para obter uma coleta por ID
        Task SaveChangesAsync();
    }
}
