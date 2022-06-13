using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Workshop.Core.Domain.SharedKernel.Repository
{
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken ct);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ct);
        Task RemoveAsync(TEntity entity);
        Task RemoveRangeAsync(IEnumerable<TEntity> entities);
        IUnitOfWork UnitOfWork { get; }
    }
}
