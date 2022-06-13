using System.Collections.Generic;
using Workshop.Core.Domain.Model.Role;
using Workshop.Core.Domain.SharedKernel.Specification;

namespace Workshop.Core.Domain.Specification
{
    public static class RoleSpecification
    {
        public static ISpecification<Role> GetById(long id) =>
            new Specification<Role>(u => u.Key == id);

        public static ISpecification<Role> GetByIds(HashSet<long> roles) =>
            new Specification<Role>(u => roles.Contains(u.Key));
    }
}