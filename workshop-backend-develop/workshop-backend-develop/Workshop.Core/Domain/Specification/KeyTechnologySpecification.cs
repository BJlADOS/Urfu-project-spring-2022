using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Core.Domain.Model.KeyTechnology;
using Workshop.Core.Domain.SharedKernel.Specification;

namespace Workshop.Core.Domain.Specification
{
    public static class KeyTechnologySpecification
    {
        public static ISpecification<KeyTechnology> GetById(long id, long eventId) => new Specification<KeyTechnology>(u => u.Key == id && u.EventId == eventId);
        public static ISpecification<KeyTechnology> GetAll(long eventId) => new Specification<KeyTechnology>(u => u.EventId == eventId);
    }
}
