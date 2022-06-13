using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Workshop.Core.Domain.SharedKernel;
using Workshop.Core.Domain.SharedKernel.Repository;

namespace Workshop.Web
{
    public static class RepositoryDependencyInjectionExtensions
    {
        public static void RegisterRepository<TEntity, TRepository, TRepositoryImpl>(
            this IServiceCollection serviceCollection)
            where TEntity : Entity, IAggregateRoot
            where TRepository : class, IRepository<TEntity>
            where TRepositoryImpl : Repository<TEntity>, TRepository
        {
            serviceCollection.AddTransient<TRepositoryImpl>();
            serviceCollection.AddTransient<TRepository>(opt => opt.GetService<TRepositoryImpl>());
            serviceCollection.AddTransient<IReadOnlyRepository<TEntity>>(opt =>
            {
                var repImpl = opt.GetService<TRepositoryImpl>();
                repImpl.ReadOnly = true;
                return repImpl;
            });
        }

        public static void RegisterRepository<TEntity, TRepository, TReadOnlyRepository, TRepositoryImpl>(
            this IServiceCollection serviceCollection)
            where TEntity : Entity, IAggregateRoot
            where TRepository : class, IRepository<TEntity>
            where TReadOnlyRepository : class, IReadOnlyRepository<TEntity>
            where TRepositoryImpl : Repository<TEntity>, TRepository, TReadOnlyRepository
        {
            serviceCollection.AddTransient<TRepositoryImpl>();
            serviceCollection.AddTransient<TRepository>(opt => opt.GetService<TRepositoryImpl>());
            serviceCollection.AddTransient<TReadOnlyRepository>(opt =>
            {
                var repImpl = opt.GetService<TRepositoryImpl>();
                repImpl.ReadOnly = true;
                return repImpl;
            });
        }
    }
}
