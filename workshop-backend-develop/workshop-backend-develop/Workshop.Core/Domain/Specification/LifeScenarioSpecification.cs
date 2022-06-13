using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Core.Domain.Model.LifeScenario;
using Workshop.Core.Domain.SharedKernel.Specification;

namespace Workshop.Core.Domain.Specification
{
    public static class LifeScenarioSpecification
    {
        public static ISpecification<LifeScenario> GetById(long id, long eventId) => new Specification<LifeScenario>(u => u.Key == id && u.EventId == eventId);
        public static ISpecification<LifeScenario> GetAll(long eventId) => new Specification<LifeScenario>(u => u.EventId == eventId);
    }
}
