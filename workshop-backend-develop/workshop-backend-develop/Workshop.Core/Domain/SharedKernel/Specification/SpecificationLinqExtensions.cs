using System.Linq;

namespace Workshop.Core.Domain.SharedKernel.Specification
{
    public static class SpecificationLinqExtensions
    {
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> query,
            ISpecification<TEntity> specification) where TEntity : Entity, IAggregateRoot
        {
            query = query.Where(specification.IsSatisfiedBy());
            return query;
        }
    }
}
