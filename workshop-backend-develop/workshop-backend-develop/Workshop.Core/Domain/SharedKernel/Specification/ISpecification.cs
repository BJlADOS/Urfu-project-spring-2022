using System;
using System.Linq.Expressions;

namespace Workshop.Core.Domain.SharedKernel.Specification
{
    public interface ISpecification<TEntity> where TEntity : Entity, IAggregateRoot
    {
        Expression<Func<TEntity, bool>> IsSatisfiedBy();
    }
}
