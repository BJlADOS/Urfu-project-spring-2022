using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Core.Domain.Model.Project;
using Workshop.Core.Domain.SharedKernel.Specification;

namespace Workshop.Core.Domain.Specification
{
    public static class ProjectSpecification
    {
        public static ISpecification<Project> GetById(long id, long eventId) =>
            new Specification<Project>(u => u.Key == id && u.EventId == eventId);

        public static ISpecification<Project> GetAll(long eventId) =>
            new Specification<Project>(u => u.EventId == eventId);

        public static ISpecification<Project> GetPromoted(long eventId) =>
            new Specification<Project>(u => u.EventId == eventId && u.IsPromoted);

        public static ISpecification<Project> GetByQuery(long? keyId, long? lifeId, string curator, string customer,
            string term, long eventId) =>
            new Specification<Project>(u => u.EventId == eventId /*&& u.IsAvailable*/
                                            && (!lifeId.HasValue || u.LifeScenarioId == lifeId.Value)
                                            && (!keyId.HasValue || u.KeyTechnologyId == keyId.Value)
                                            && (string.IsNullOrWhiteSpace(curator) || u.Curator.Contains(curator))
                                            && (string.IsNullOrWhiteSpace(customer) ||
                                                u.Organization.Contains(customer))
                                            && (string.IsNullOrWhiteSpace(term)
                                                || u.Name.Contains(term)
                                                || u.Organization.Contains(term)
                                                || u.Curator.Contains(term)
                                                || u.Contacts.Contains(term)
                                                || u.Purpose.Contains(term)
                                                || u.Result.Contains(term)));

        public static ISpecification<Project> GetByScenario(long scenarioId, long eventId) =>
            new Specification<Project>(p => p.EventId == eventId && p.LifeScenarioId == scenarioId);

        public static ISpecification<Project> GetByTechnology(long technologyId, long eventId) =>
            new Specification<Project>(p => p.EventId == eventId && p.KeyTechnologyId == technologyId);

        public static ISpecification<Project> GetByOrganization(string organization, long eventId) =>
            new Specification<Project>(p => p.EventId == eventId && p.Organization != null && p.Organization == organization);
    }
}