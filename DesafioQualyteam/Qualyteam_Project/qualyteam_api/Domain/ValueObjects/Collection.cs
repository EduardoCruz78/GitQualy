using System;

namespace qualyteam_api.Domain.Entities;

public class Collection
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public double Value { get; set; }
    public Guid IndicatorId { get; set; }

    public required Indicator Indicator { get; set; }
}