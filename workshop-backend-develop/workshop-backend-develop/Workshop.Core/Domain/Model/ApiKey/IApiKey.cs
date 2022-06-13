using System;

namespace Workshop.Core.Domain.Model.ApiKey
{
    public interface IApiKey
    {
        long Key { get; }
        string KeyString { get; }
        long EventId { get; }
        DateTime CreationDate { get; }
        string Comment { get; }
    }
}