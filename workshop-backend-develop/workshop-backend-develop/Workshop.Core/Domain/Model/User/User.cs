using System;
using System.Collections.Generic;
using Workshop.Core.Domain.SharedKernel;
using Workshop.Core.Helpers;

namespace Workshop.Core.Domain.Model.User
{
    public class User : Entity<long>, IUser, IAggregateRoot
    {
        //TODO ValueObject
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string About { get; set; }
        public string AcademicGroup { get; set; }

        public string Login { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        public string Password { get; private set; }
        public string Salt { get; private set; }
        public DateTime RegistrationDate { get; private set; }

        public virtual HashSet<UserCompetency.UserCompetency> Competencies { get; private set; }
        public long? TeamId { get; private set; }
        public Team.Team Team { get; set; }

        public long? RoleId { get; private set; }
        public Role.Role Role { get; private set; }

        public virtual List<UserAuditorium.UserAuditorium> Auditoriums { get; set; }

        public UserType UserType { get; private set; }
        public long EventId { get; private set; }
        public bool ProfileFilled { get; private set; }
        public bool IsShowContactsToExpert { get; set; }

        public User()
        {
        }

        public User(string login, string password, string salt, UserType userType, long eventId)
        {
            Login = login;
            Password = password;
            Salt = salt;
            UserType = userType;
            EventId = eventId;
            RegistrationDate = DateTimeHelper.GetCurrentTime();
            Competencies = new HashSet<UserCompetency.UserCompetency>();
            Auditoriums = new List<UserAuditorium.UserAuditorium>();
            IsShowContactsToExpert = true;
        }


        public User(string login, string password, string salt, UserType userType, long eventId, string email,
            string lastName, string firstName, string middleName, string academicGroup) : this(login, password, salt,
            userType, eventId)
        {
            Email = email;
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            AcademicGroup = academicGroup;
            IsShowContactsToExpert = true;
        }

        public void UpdateFirstName(string name)
        {
            FirstName = name;
        }

        public void UpdateLastName(string name)
        {
            LastName = name;
        }

        public void UpdateMiddleName(string name)
        {
            MiddleName = name;
        }

        public void UpdateEmail(string email)
        {
            Email = email;
        }

        public void UpdatePhoneNumber(string phone)
        {
            PhoneNumber = phone;
        }

        public void ResetPassword(string password, string salt)
        {
            Password = password;
            Salt = salt;
        }

        public void UpdateAcademicGroup(string academicGroup)
        {
            AcademicGroup = academicGroup;
        }

        public void UpdateAbout(string about)
        {
            About = about;
        }

        public void UpdateTeam(long teamId)
        {
            TeamId = teamId;
        }

        public void LeaveTeam()
        {
            TeamId = null;
            RoleId = null;
        }

        public void UpdateCompetencies(HashSet<UserCompetency.UserCompetency> competencies)
        {
            Competencies = competencies;
        }

        public void UpdateProfileFilled(bool filled)
        {
            ProfileFilled = filled;
        }

        public void UpdateRole(long roleId)
        {
            RoleId = roleId;
        }

        public void UpdateUserType(UserType userType)
        {
            UserType = userType;
        }
        
        public void UpdateIsShowContactsToExpert(bool isShowContactsToExpert)
        {
            IsShowContactsToExpert = isShowContactsToExpert;
        }
    }
}