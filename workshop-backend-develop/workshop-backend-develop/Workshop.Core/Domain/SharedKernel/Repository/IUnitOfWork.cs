using System;
using System.Threading;
using System.Threading.Tasks;

namespace Workshop.Core.Domain.SharedKernel.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
