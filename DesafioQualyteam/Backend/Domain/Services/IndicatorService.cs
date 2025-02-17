using Backend.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Backend.Domain.Services;

public class IndicadorService
{
    private readonly ApplicationDbContext _context;

    public IndicadorService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Método para listar indicadores
    public async Task<IEnumerable<Indicador>> GetIndicadoresAsync()
    {
        return await _context.Indicadores.ToListAsync();
    }

    // Método para cadastrar um indicador com inicializador de objeto
    public async Task<Indicador> CadastrarIndicadorAsync(string nome, string formaCalculo)
    {
        var indicador = new Indicador
        {
            Nome = nome,
            FormaCalculo = formaCalculo
        };

        _context.Indicadores.Add(indicador);
        await _context.SaveChangesAsync(); // Aguarda a conclusão da operação assíncrona
        return indicador;
    }


    // Método para registrar uma coleta
    public async Task RegistrarColetaAsync(int indicadorId, DateTime data, decimal valor)
    {
        var indicador = await _context.Indicadores.FindAsync(indicadorId);
        if (indicador == null) throw new Exception("Indicador não encontrado.");

        var coleta = new Coleta
        {
            IndicadorId = indicadorId, // Inicializa a propriedade required
            Data = data,
            Valor = valor,
            Indicador = indicador
        };

        _context.Coletas.Add(coleta);
        await _context.SaveChangesAsync(); // Aguarda a conclusão da operação assíncrona
    }

    // Método para calcular o resultado de um indicador
    public async Task<decimal> CalcularResultadoAsync(int indicadorId)
    {
        var indicador = await _context.Indicadores
            .Include(i => i.Coletas)
            .FirstOrDefaultAsync(i => i.Id == indicadorId);

        if (indicador == null) throw new Exception("Indicador não encontrado.");

        var valores = indicador.Coletas.Select(c => c.Valor);

        return indicador.FormaCalculo switch
        {
            "SOMA" => valores.Sum(),
            "MÉDIA" => valores.Average(),
            _ => throw new Exception("Forma de cálculo inválida.")
        };
    }
}
