using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Workshop.Core.Domain.SharedKernel.Specification
{
    public class Specification<TEntity> : ISpecification<TEntity> where TEntity : Entity, IAggregateRoot
    {
        private readonly Expression<Func<TEntity, bool>> _expression;

        public Specification(Expression<Func<TEntity, bool>> expression)
        {
            _expression = expression;
        }
        public Expression<Func<TEntity, bool>> IsSatisfiedBy()
        {
            return _expression;
        }

    }
}
