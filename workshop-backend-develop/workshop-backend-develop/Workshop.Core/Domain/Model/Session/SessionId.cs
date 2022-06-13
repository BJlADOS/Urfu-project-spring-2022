using System;
using System.Collections.Generic;
using Workshop.Core.Domain.SharedKernel;

namespace Workshop.Core.Domain.Model.Session
{
    public class SessionId : ValueObject
    {
        public Guid Id { get; private set; }

        private SessionId()
        {
        }

        public SessionId(Guid id)
        {
            Id = id;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
        }
    }
}