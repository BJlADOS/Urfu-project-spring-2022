using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Core.Domain.Model.Session;

namespace Workshop.Infrastructure.Storage.Repositories
{
    public class SessionRepository : EFRepository<Session>, ISessionRepository
    {
        public SessionRepository(WorkshopDbContext context) : base(context)
        {
        }
    }
}
