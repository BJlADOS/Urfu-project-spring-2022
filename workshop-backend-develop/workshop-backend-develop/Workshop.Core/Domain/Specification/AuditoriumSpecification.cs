using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Core.Domain.Model.Auditorium;
using Workshop.Core.Domain.SharedKernel.Specification;

namespace Workshop.Core.Domain.Specification
{
    public static class AuditoriumSpecification
    {
        public static ISpecification<Auditorium> GetById(long id, long eventId) => new Specification<Auditorium>(u => u.Key == id && u.EventId == eventId);
        public static ISpecification<Auditorium> GetAll(long eventId) => new Specification<Auditorium>(u => u.EventId == eventId);
    }
}