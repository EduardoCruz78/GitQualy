using System;

namespace qualyteam_api.Domain.Entities
{
    public class Collection
    {
        public DateTime Date { get; private set; }
        public decimal Value { get; set; }

        public Collection(DateTime date, decimal value)
        {
            Date = date;
            Value = value;
        }
    }
}