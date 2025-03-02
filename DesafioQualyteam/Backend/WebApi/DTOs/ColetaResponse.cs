namespace Backend.WebApi.DTOs
{
    public record ColetaResponse(int Id, DateTime Data, decimal Valor, int IndicadorId);
}