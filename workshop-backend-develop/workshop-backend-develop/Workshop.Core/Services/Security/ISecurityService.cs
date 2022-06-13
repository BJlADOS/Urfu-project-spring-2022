using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Workshop.Core.Services.Security
{
    public interface ISecurityService
    {
        Task Login(string login, string password, CancellationToken cancellationToken);
        Task Login(Guid sid, CancellationToken cancellationToken);
        Task Logout(CancellationToken cancellationToken);
    }
}
