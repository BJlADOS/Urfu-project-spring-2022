using System;
using Workshop.Core.Domain.SharedKernel;

namespace Workshop.Core.Domain.Model.Session
{
    public class Session : Entity<SessionId>, IAggregateRoot
    {
        public long UserId { get; private set; }
        public DateTime Expires { get; private set; }
        public DateTime LastConnection { get; private set; }

        private Session()
        {
        }

        public Session(long userId) : base(new SessionId(Guid.NewGuid()))
        {
            UserId = userId;

            var now = DateTime.Now;
            Expires = now.Date.AddDays(30);
            LastConnection = now;
        }

        public void ChangeLastConnectionDate(DateTime lastConnection)
        {
            LastConnection = lastConnection;
        }

        public void ChangeSidExpireDate(DateTime sidExpireDate)
        {
            Expires = sidExpireDate;
        }

        public void UpdateUserId(long userId)
        {
            UserId = userId;
        }
    }
}