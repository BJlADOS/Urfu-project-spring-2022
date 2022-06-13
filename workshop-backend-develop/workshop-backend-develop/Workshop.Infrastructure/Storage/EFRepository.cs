using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.SharedKernel;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.SharedKernel.Specification;
using Microsoft.EntityFrameworkCore;

namespace Workshop.Infrastructure.Storage
{
    public class EFRepository<TEntity> : Repository<TEntity>
            where TEntity : Entity, IAggregateRoot
    {
        protected WorkshopDbContext Context { get; }
        private DbSet<TEntity> _items => Context.Set<TEntity>();
        protected virtual IQueryable<TEntity> Items => ReadOnly ? _items.AsNoTracking() : _items;
        protected EFRepository(WorkshopDbContext context) : base(context)
        {
            Context = context;
        }

        public override async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct)
        {
            var entry = await _items.AddAsync(entity, ct);
            return entry.Entity;
        }

        public override Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            return _items.AddRangeAsync(entities, ct);
        }

        public override Task RemoveAsync(TEntity entity)
        {
            _items.Remove(entity);
            return Task.CompletedTask;
        }

        public override Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            _items.RemoveRange(entities);
            return Task.CompletedTask;
        }




        public override Task<TEntity[]> ListAsync(CancellationToken cancellationToken)
        {
            return Items.ToArrayAsync(cancellationToken);
        }

        public override Task<TEntity[]> ListAsync(ISpecification<TEntity> specification,
            CancellationToken cancellationToken)
        {
            return Items.Where(specification.IsSatisfiedBy()).ToArrayAsync(cancellationToken);
                
        }

        public override Task<TEntity> SingleAsync(CancellationToken cancellationToken)
        {
            return Items.SingleAsync(cancellationToken);
        }

        public override Task<TEntity> SingleAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken)
        {
            return Items.SingleAsync(specification.IsSatisfiedBy(), cancellationToken);
        }

        public override Task<TEntity> SingleOrDefaultAsync(CancellationToken cancellationToken)
        {
            return Items.SingleOrDefaultAsync(cancellationToken);
        }

        public override Task<TEntity> SingleOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken)
        {
            return Items.SingleOrDefaultAsync(specification.IsSatisfiedBy(), cancellationToken);
        }

        public override Task<TEntity> FirstAsync(CancellationToken cancellationToken)
        {
            return Items.FirstAsync(cancellationToken);
        }

        public override Task<TEntity> FirstAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken)
        {
            return Items.FirstAsync(specification.IsSatisfiedBy(), cancellationToken);
        }

        public override Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken)
        {
            return Items.FirstOrDefaultAsync(cancellationToken);
        }

        public override Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken)
        {
            return Items.FirstOrDefaultAsync(specification.IsSatisfiedBy(), cancellationToken);
        }

        public override Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return Items.CountAsync(cancellationToken);
        }

        public override Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken)
        {
            return Items.CountAsync(specification.IsSatisfiedBy(), cancellationToken);
        }

        public override Task<long> LongCountAsync(CancellationToken cancellationToken)
        {
            return Items.LongCountAsync(cancellationToken);
        }

        public override Task<long> LongCountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken)
        {
            return Items.LongCountAsync(specification.IsSatisfiedBy(), cancellationToken);
        }

        public override Task<TResult[]> Query<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query, CancellationToken cancellationToken)
        {
            return query(Items).ToArrayAsync(cancellationToken);
        }


    }
}
