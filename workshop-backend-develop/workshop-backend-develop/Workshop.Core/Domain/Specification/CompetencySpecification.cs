using System.Collections.Generic;
using Workshop.Core.Domain.Model.Competency;
using Workshop.Core.Domain.SharedKernel.Specification;

namespace Workshop.Core.Domain.Specification
{
    public static class CompetencySpecification
    {
        public static ISpecification<Competency> GetById(long id) => new Specification<Competency>(u => u.Key == id);
        public static ISpecification<Competency> GetByTerm(string term) => new Specification<Competency>(u => string.IsNullOrWhiteSpace(term) || u.Name.Contains(term));

        public static ISpecification<Competency> GetByIds(HashSet<long> competencies) =>
            new Specification<Competency>(u => competencies.Contains(u.Key));
    }
}
