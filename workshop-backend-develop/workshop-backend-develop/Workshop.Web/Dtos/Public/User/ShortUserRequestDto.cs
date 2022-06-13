using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.RequestProposal;
using Workshop.Web.Dtos.Public.Competency;
using Workshop.Web.Dtos.Public.Role;

namespace Workshop.Web.Dtos.Public.User
{
    public class ShortUserRequestDto
    {
        public long Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string AcademicGroup { get; set; }
        public ICollection<UserCompetencyDto> Competencies { get; set; }
        public string About { get; set; }
        public string RoleName { get; set; }
        public RequestStatus Status { get; set; }   
    }
}
