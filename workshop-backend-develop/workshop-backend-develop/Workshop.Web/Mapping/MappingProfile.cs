using AutoMapper;
using Workshop.Core.Domain.Model.ApiKey;
using Workshop.Core.Domain.Model.Auditorium;
using Workshop.Core.Domain.Model.Competency;
using Workshop.Core.Domain.Model.Event;
using Workshop.Core.Domain.Model.KeyTechnology;
using Workshop.Core.Domain.Model.LifeScenario;
using Workshop.Core.Domain.Model.Project;
using Workshop.Core.Domain.Model.ProjectCompetency;
using Workshop.Core.Domain.Model.ProjectProposal;
using Workshop.Core.Domain.Model.RequestProposal;
using Workshop.Core.Domain.Model.Role;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Model.TeamCompetencyReview;
using Workshop.Core.Domain.Model.TeamReview;
using Workshop.Core.Domain.Model.TeamSlot;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Model.UserAuditorium;
using Workshop.Core.Domain.Model.UserCompetency;
using Workshop.Web.Dtos.Admin.ApiKey;
using Workshop.Web.Dtos.Expert.Event;
using Workshop.Web.Dtos.Expert.Team;
using Workshop.Web.Dtos.Expert.User;
using Workshop.Web.Dtos.Public.Auditorium;
using Workshop.Web.Dtos.Public.Competency;
using Workshop.Web.Dtos.Public.Event;
using Workshop.Web.Dtos.Public.KeyTechnology;
using Workshop.Web.Dtos.Public.LifeScenario;
using Workshop.Web.Dtos.Public.Project;
using Workshop.Web.Dtos.Public.ProjectProposal;
using Workshop.Web.Dtos.Public.RequestProposal;
using Workshop.Web.Dtos.Public.Role;
using Workshop.Web.Dtos.Public.Team;
using Workshop.Web.Dtos.Public.TeamSlot;
using Workshop.Web.Dtos.Public.User;

namespace Workshop.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key));

            CreateMap<User, ShortUserDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key))
                //.ForMember(x => x.Competencies, opt => opt.MapFrom(m => m.Competencies));
                .ForPath(x => x.Competencies, opt => opt.MapFrom(m => m.Competencies));

            CreateMap<User, ProfileUserDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key));

            CreateMap<User, ExpertUserDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key));

            CreateMap<User, UsersListItemDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key));

            CreateMap<User, UserDetailedDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key));

            CreateMap<LifeScenario, LifeScenarioDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key));

            CreateMap<KeyTechnology, KeyTechnologyDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key));

            CreateMap<Project, ProjectDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key));

            CreateMap<Project, ShortProjectDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key));

                CreateMap<User, ShortUserRequestDto>()
                .ForMember(x => x.Competencies, opt => opt.MapFrom(m => m.Competencies))
                .ForMember(x=>x.Id,opt=>opt.MapFrom(m=>m.Key));

            CreateMap<RequestProposal, UserRequestProposalDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key))
                .ForMember(x => x.RoleName, opt => opt.MapFrom(m=> m.RoleName))
                .ForMember(x=>x.RequestStatus, opt => opt.MapFrom(m=>m.Status));

            CreateMap<RequestProposal, ShortUserRequestDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key))
                .ForMember(x => x.RoleName, opt => opt.MapFrom(m => m.RoleName));
                



            CreateMap<Project, UserRequestProposalDto>()
                .ForMember(x => x.Name, opt => opt.MapFrom(m => m.Name))
                .ForMember(x=>x.Result, opt => opt.MapFrom(m=>m.Result))
                .ForMember(x => x.Contacts, opt => opt.MapFrom(m => m.Contacts))
                .ForMember(x => x.Purpose, opt => opt.MapFrom(m => m.Purpose))
                .ForMember(x => x.TeamCapacity, opt => opt.MapFrom(m => m.TeamCapacity))
                .ForMember(x => x.LifeScenarioName, opt => opt.MapFrom(m => m.LifeScenario.Name))
                .ForMember(x => x.KeyTechnologyName, opt => opt.MapFrom(m => m.KeyTechnology.Name))
                .ForMember(x => x.Description, opt => opt.MapFrom(m => m.Description));


            CreateMap<ProjectCompetency, CompetencyDto>()
                .ForMember(x => x.CompetencyType, opt => opt.MapFrom(m => m.Competency.CompetencyType))
                .ForMember(x => x.Name, opt => opt.MapFrom(m => m.Competency.Name))
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Competency.Key));
           
            
            CreateMap<Competency, CompetencyDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key));

            CreateMap<UserCompetency, UserCompetencyDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.CompetencyId))
                .ForMember(x => x.Name, opt => opt.MapFrom(m => m.Competency.Name))
                .ForMember(x => x.CompetencyType, opt => opt.MapFrom(m => m.Competency.CompetencyType))
                .ForMember(x => x.UserCompetencyType, opt => opt.MapFrom(m => m.UserCompetencyType));

            CreateMap<Team, TeamDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key))
                .ForMember(x => x.IsEntried, opt => opt.MapFrom(m => m.IsEntried)); 

            CreateMap<Team, ShortTeamDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key));
                
            CreateMap<Team, UserRequestProposalDto>()
                .ForMember(x => x.Team, opt => opt.MapFrom(m => m));

            CreateMap<Team, TeamListItemDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key))
                .ForMember(x => x.UsersCount, opt => opt.MapFrom(m => m.Users.Count))
                .ForMember(x => x.ProjectName, opt => opt.MapFrom(m => m.Project.Name))
                .ForMember(x => x.TeamCapacity, opt => opt.MapFrom(m => m.Project.TeamCapacity))
                .ForMember(x => x.ProjectDescription, opt => opt.MapFrom(m => m.Project.Purpose));

            CreateMap<Team, TeamWithSlotListDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key))
                .ForMember(x => x.UsersCount, opt => opt.MapFrom(m => m.Users.Count))
                .ForMember(x => x.ProjectName, opt => opt.MapFrom(m => m.Project.Name))
                .ForMember(x => x.TeamCapacity, opt => opt.MapFrom(m => m.Project.TeamCapacity))
                .ForMember(x => x.ProjectDescription, opt => opt.MapFrom(m => m.Project.Purpose))
                .ForMember(x => x.AuditoriumId, opt => opt.MapFrom(m => m.TeamSlot.AuditoriumId))
                .ForMember(x => x.AuditoriumName, opt => opt.MapFrom(m => m.TeamSlot.Auditorium.Name))
                .ForMember(x => x.SlotTime, opt => opt.MapFrom(m => m.TeamSlot.Time));

            CreateMap<Auditorium, AuditoriumDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key));

            CreateMap<Competency, CompetencyDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key));

            CreateMap<Role, RoleDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key));

            CreateMap<Event, EventDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key));

            CreateMap<Event, EventFullDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key));

            CreateMap<TeamReview, TeamReviewDto>();

            CreateMap<TeamCompetencyReview, TeamCompetencyReviewDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.CompetencyId));

            CreateMap<ProjectProposal, ProposalDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(proposal => proposal.Key))
                .ForMember(dto => dto.Status, opt => opt.MapFrom(proposal => proposal.Status));

            CreateMap<ProjectProposal, GetPendingProposals>()
               .ForMember(dto => dto.Id, opt => opt.MapFrom(proposal => proposal.Key))
               .ForMember(dto => dto.Status, opt => opt.MapFrom(proposal => proposal.Status))
               .ForPath(dto => dto.Author.Id, opt => opt.MapFrom(proposal => proposal.Author.Key))
               .ForPath(dto => dto.Author.FirstName, opt => opt.MapFrom(proposal => proposal.Author.FirstName))
               .ForPath(dto => dto.Author.MiddleName, opt => opt.MapFrom(proposal => proposal.Author.MiddleName))
               .ForPath(dto => dto.Author.LastName, opt => opt.MapFrom(proposal => proposal.Author.LastName))
               .ForPath(dto => dto.Author.AcademicGroup, opt => opt.MapFrom(proposal => proposal.Author.AcademicGroup));

            CreateMap<RequestProposal, RequestProposalDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(request => request.Key))
                .ForMember(dto => dto.Status, opt => opt.MapFrom(request => request.Status));

            CreateMap<ApiKey, ApiKeyDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key));

            CreateMap<TeamSlot, TeamSlotDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key));

            CreateMap<Auditorium, AuditoriumDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Key))
                .ForMember(x => x.Slots, opt => opt.MapFrom(m => m.TeamSlots));

            CreateMap<UserAuditorium, AuditoriumDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.AuditoriumId))
                .ForMember(x => x.Name, opt => opt.MapFrom(m => m.Auditorium.Name));

            CreateMap<UserAuditorium, ExpertUserDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.UserId))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(m => m.User.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(m => m.User.LastName))
                .ForMember(x => x.MiddleName, opt => opt.MapFrom(m => m.User.MiddleName));
        }
    }
}
