using System;
using System.Collections.Generic;
using Workshop.Core.Domain.Model.Team;
using Workshop.Web.Dtos.Public.Project;
using Workshop.Web.Dtos.Public.TeamSlot;
using Workshop.Web.Dtos.Public.User;

namespace Workshop.Web.Dtos.Public.Team
{
    public class ShortTeamDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public TeamStatus TeamStatus { get; set; }
        public ICollection<ShortUserDto> Users { get; set; }
        public string PMSLink { get; set; }
        public string RepositoryLink { get; set; }
        public string AdditionalLink { get; set; }
    }
}
