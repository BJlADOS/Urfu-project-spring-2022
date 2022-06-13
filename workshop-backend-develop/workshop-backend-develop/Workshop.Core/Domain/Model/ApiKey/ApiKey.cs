using System;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.SharedKernel;
using Workshop.Core.Helpers;

namespace Workshop.Core.Domain.Model.ApiKey
{
    public class ApiKey : Entity<long>, IAggregateRoot, IApiKey
    {
        public string Name { get; private set; }
        public string KeyString { get; private set; }
        public UserType UserType { get; private set; }
        public long EventId { get; set; }
        public DateTime CreationDate { get; }
        public string Comment { get; private set; }

        public ApiKey(string name, string keyString, UserType userType, long eventId)
        {
            Name = name;
            KeyString = keyString;
            UserType = userType;
            EventId = eventId;
            CreationDate = DateTimeHelper.GetCurrentTime();
        }

        public void UpdateKeyString(string keyString)
        {
            KeyString = keyString;
        }

        public void UpdateUserType(UserType userType)
        {
            UserType = userType;
        }

        public void UpdateEventId(long eventId)
        {
            EventId = eventId;
        }

        public void UpdateComment(string comment)
        {
            Comment = comment;
        }
    }
}