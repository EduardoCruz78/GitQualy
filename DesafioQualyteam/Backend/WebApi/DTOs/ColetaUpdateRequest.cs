// Arquivo: Backend/WebApi/DTOs/ColetaUpdateRequest.cs

using System;

namespace Backend.WebApi.DTOs
{
    public record ColetaUpdateRequest(DateTime Data, decimal Valor);
}
