﻿using System.Collections.Generic;
using Workshop.Web.Dtos.Public.Competency;
using Workshop.Web.Dtos.Public.KeyTechnology;
using Workshop.Web.Dtos.Public.LifeScenario;
using Workshop.Web.Dtos.Public.Role;

namespace Workshop.Web.Dtos.Public.Project
{
    public class ShortProjectDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Curator { get; set; }
        public string Organization { get; set; }
        public string Contacts { get; set; }
        public string Purpose { get; set; }
        public string Result { get; set; }
        public int TeamCapacity { get; set; }
        public int MaxTeamCount { get; set; }

        public LifeScenarioDto LifeScenario { get; set; }
        public KeyTechnologyDto KeyTechnology { get; set; }
        public ICollection<CompetencyDto> Competencies { get; set; }
        public ICollection<RoleDto> Roles { get; set; }
        public int FillTeamsCount { get; set; }
        public int ParticipantsCount { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsPromoted { get; set; }

        public  bool IsEntryOpen { get; set; }
    }
}
