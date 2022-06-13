using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Core.Domain.SharedKernel.Repository;

namespace Workshop.Core.Domain.Model.User
{
    public interface IUserRepository : IRepository<User>
    {
    }
}