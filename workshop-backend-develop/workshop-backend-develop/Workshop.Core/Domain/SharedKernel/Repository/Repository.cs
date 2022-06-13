using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Workshop.Core.Domain.SharedKernel.Repository
{
    public abstract class Repository<TEntity> : ReadOnlyRepository<TEntity>, IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        private readonly IUnitOfWork _unitOfWork;

        protected Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public abstract Task<TEntity> AddAsync(TEntity entity, CancellationToken ct);
        public abstract Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ct);
        public abstract Task RemoveAsync(TEntity entity);
        public abstract Task RemoveRangeAsync(IEnumerable<TEntity> entities);
        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (ReadOnly) { throw new NotImplementedException("For current repository enabled readonly mode"); }

                return _unitOfWork;
            }
        }
    }
}
