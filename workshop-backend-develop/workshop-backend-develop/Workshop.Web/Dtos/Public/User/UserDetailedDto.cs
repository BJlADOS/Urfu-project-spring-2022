using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.User;
using Workshop.Web.Dtos.Public.Competency;
using Workshop.Web.Dtos.Public.Project;
using Workshop.Web.Dtos.Public.Role;
using Workshop.Web.Dtos.Public.Team;

namespace Workshop.Web.Dtos.Public.User
{
    public class UserDetailedDto
    {
        public long Id { get; set; }
        public long EventId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string About { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AcademicGroup { get; set; }
        public UserType UserType { get; set; }
        public DateTime RegistrationDate { get; set; }

        public ShortTeamDto Team { get; set; }
        public ShortProjectDto Project { get; set; }
        public ICollection<UserCompetencyDto> Competencies { get; set; }
        public bool ProfileFilled { get; set; }

        public RoleDto Role { get; set; }
        public bool IsShowContactsToExpert { get; set; }
    }
}
