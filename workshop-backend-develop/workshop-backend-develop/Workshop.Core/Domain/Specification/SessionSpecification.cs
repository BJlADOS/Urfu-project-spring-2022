using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Core.Domain.Model.Session;
using Workshop.Core.Domain.SharedKernel.Specification;

namespace Workshop.Core.Domain.Specification
{
    public static class SessionSpecification
    {
        public static ISpecification<Session> GetSession(SessionId sessionId) => new Specification<Session>(u => u.Key.Id == sessionId.Id);
    }
}
