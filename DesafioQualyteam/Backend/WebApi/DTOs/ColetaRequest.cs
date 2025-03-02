namespace Backend.WebApi.DTOs
{
    public record ColetaRequest(int IndicadorId, DateTime Data, decimal Valor);   
}