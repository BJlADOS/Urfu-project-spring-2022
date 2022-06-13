using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Core.Domain.Model.ApiKey;
using Workshop.Core.Domain.SharedKernel.Specification;

namespace Workshop.Core.Domain.Specification
{
    public static class ApiKeySpecification
    {
        public static ISpecification<ApiKey> GetById(long id) =>
            new Specification<ApiKey>(u => u.Key == id);

        public static ISpecification<ApiKey> GetByKeyString(string keyString) =>
            new Specification<ApiKey>(u => u.KeyString == keyString);

        public static ISpecification<ApiKey> GetByEventId(long eventId) =>
            new Specification<ApiKey>(u => u.EventId == eventId);

        public static ISpecification<ApiKey> GetAll() =>
            new Specification<ApiKey>(u => u != null);
    }
}
