using System;
using System.Linq.Expressions;

namespace Workshop.Core.Domain.SharedKernel.Specification
{
    public static class SpecificationExtensions
    {
        public static ISpecification<TEntity> And<TEntity>(this ISpecification<TEntity> specLeft,
            ISpecification<TEntity> specRight) where TEntity : Entity, IAggregateRoot
        {
            if (specLeft == null)
            {
                return specRight;
            }

            var specLeftExpresion = specLeft.IsSatisfiedBy();
            var specRightExpression = specRight.IsSatisfiedBy();

            var andAlsoExpression = Expression.AndAlso(specLeftExpresion.Body,
                specRightExpression.Body);

            //Заменяем ссылку 
            var parameter = Expression.Parameter(typeof(TEntity));
            andAlsoExpression = (BinaryExpression)new ParameterReplacer(parameter).Visit(andAlsoExpression);

            Expression<Func<TEntity, bool>> andExpression = Expression.Lambda<Func<TEntity, bool>>(andAlsoExpression, parameter);
            return new Specification<TEntity>(andExpression);
        }

        public static ISpecification<TEntity> Or<TEntity>(this ISpecification<TEntity> specLeft,
            ISpecification<TEntity> specRight) where TEntity : Entity, IAggregateRoot
        {
            if (specLeft == null)
            {
                return specRight;
            }

            var specLeftExpresion = specLeft.IsSatisfiedBy();
            var specRightExpression = specRight.IsSatisfiedBy();

            var andAlsoExpression = Expression.OrElse(specLeftExpresion.Body,
                specRightExpression.Body);

            var parameter = Expression.Parameter(typeof(TEntity));
            andAlsoExpression = (BinaryExpression)new ParameterReplacer(parameter).Visit(andAlsoExpression);

            Expression<Func<TEntity, bool>> andExpression = Expression.Lambda<Func<TEntity, bool>>(andAlsoExpression, parameter);
            return new Specification<TEntity>(andExpression);
        }

        //private static  Combine
        internal class ParameterReplacer : ExpressionVisitor
        {
            private readonly ParameterExpression _parameter;

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return base.VisitParameter(_parameter);
            }

            internal ParameterReplacer(ParameterExpression parameter)
            {
                _parameter = parameter;
            }
        }

    }
}
