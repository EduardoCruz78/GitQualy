// Arquivo: Backend/Domain/Interfaces/IIndicadorRepository.cs

using Backend.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Domain.Interfaces
{
    public interface IIndicadorRepository
    {
        Task<IEnumerable<Indicador>> GetAllAsync();
        
        // Retorna Indicador? para indicar que pode ser nulo
        Task<Indicador?> GetByIdAsync(int id);

        Task AddIndicadorAsync(Indicador indicador);
        Task AddColetaAsync(Coleta coleta);
        Task SaveChangesAsync();
    }
}
