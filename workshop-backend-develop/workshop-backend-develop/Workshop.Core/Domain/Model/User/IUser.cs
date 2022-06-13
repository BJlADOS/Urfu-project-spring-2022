using System;
using System.Collections.Generic;
using System.Text;

namespace Workshop.Core.Domain.Model.User
{
    public interface IUser
    {
        long Key { get; }

        //TODO ValueObject
        string LastName { get; }
        string FirstName { get; }
        string MiddleName { get; }
        string Login { get; }
        string Email { get; }
        string PhoneNumber { get; }
        DateTime RegistrationDate { get; }
    }
}