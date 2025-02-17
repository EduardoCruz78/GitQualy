using System;
using System.Collections.Generic;
using qualyteam_api.Domain.ValueObjects;

namespace qualyteam_api.Domain.Entities;

public class Indicator
{
    public Guid Id { get; set; }
    public required string Name { get; set; } // Usando 'required' para garantir que o campo seja preenchido
    public CalculationType CalculationType { get; set; }
    public ICollection<Collection> Collections { get; set; } = new List<Collection>();
}