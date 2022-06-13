using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.ApiKey;
using Workshop.Core.Domain.SharedKernel.Specification;

namespace Workshop.Infrastructure.Storage.Repositories
{
    public class ApiKeyRepository : EFRepository<ApiKey>, IApiKeyRepository
    {
        public ApiKeyRepository(WorkshopDbContext context) : base(context)
        {
        }
    }
}
