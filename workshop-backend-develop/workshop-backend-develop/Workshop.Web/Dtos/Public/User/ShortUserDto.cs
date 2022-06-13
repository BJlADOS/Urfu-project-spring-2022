using System.Collections.Generic;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Model.UserCompetency;
using Workshop.Web.Dtos.Public.Competency;
using Workshop.Web.Dtos.Public.Role;
using Workshop.Web.Dtos.Public.Team;

namespace Workshop.Web.Dtos.Public.User
{
    public class ShortUserDto
    {
        public long Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string AcademicGroup { get; set; }
        public ICollection<UserCompetencyDto> Competencies { get; set; }
        public RoleDto Role { get; set; }

        public ShortUserDto(ShortUserDto self, List<UserCompetencyDto> competencies)
        {
            Id = self.Id;
            LastName = self.LastName;
            MiddleName = self.MiddleName;
            FirstName = self.FirstName;
            AcademicGroup = self.AcademicGroup;
            Role = self.Role;
            Competencies = competencies;
        }
        public ShortUserDto(){}
    }
}