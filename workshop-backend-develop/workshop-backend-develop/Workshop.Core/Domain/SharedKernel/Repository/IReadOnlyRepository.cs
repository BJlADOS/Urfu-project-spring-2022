using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.SharedKernel.Specification;

namespace Workshop.Core.Domain.SharedKernel.Repository
{
    public interface IReadOnlyRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        bool ReadOnly { get; }
        Task<TEntity[]> ListAsync(CancellationToken cancellationToken);
        Task<TEntity[]> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);
        Task<TEntity> SingleAsync(CancellationToken cancellationToken);

        Task<TEntity> SingleAsync(ISpecification<TEntity> specification,
            CancellationToken cancellationToken);
        Task<TEntity> SingleOrDefaultAsync(CancellationToken cancellationToken);
        Task<TEntity> SingleOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);
        Task<TEntity> FirstAsync(CancellationToken cancellationToken);
        Task<TEntity> FirstAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);
        Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken);
        Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);
        Task<int> CountAsync(CancellationToken cancellationToken);
        Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);
        Task<long> LongCountAsync(CancellationToken cancellationToken);
        Task<long> LongCountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);
        Task<TResult[]> Query<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query,
            CancellationToken cancellationToken);
    }
}
