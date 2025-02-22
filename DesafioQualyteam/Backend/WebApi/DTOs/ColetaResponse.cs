// Arquivo: Backend/WebApi/DTOs/ColetaResponse.cs

using System;

namespace Backend.WebApi.DTOs
{
    public record ColetaResponse(int Id, DateTime Data, decimal Valor, int IndicadorId);
}
