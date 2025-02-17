using System;
using System.Collections.Generic;
using qualyteam_api.Domain.Entities;

namespace qualyteam_api.Application.Interfaces
{
    public interface IIndicatorRepository
    {
        Indicator GetById(Guid id);
        void Add(Indicator indicator);
        void Update(Indicator indicator);
        void Delete(Guid id);
        IEnumerable<Indicator> GetAll();
    }
}