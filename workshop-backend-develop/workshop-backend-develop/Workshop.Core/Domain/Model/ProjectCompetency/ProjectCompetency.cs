using Workshop.Core.Domain.SharedKernel;

namespace Workshop.Core.Domain.Model.ProjectCompetency
{
    public class ProjectCompetency : IAggregateRoot, IProjectCompetency
    {
        public long ProjectId { get; }
        public long CompetencyId { get; }
        public virtual Project.Project Project { get; }
        public virtual Competency.Competency Competency { get; }

        public ProjectCompetency(long projectId, long competencyId)
        {
            ProjectId = projectId;
            CompetencyId = competencyId;
        }

        public ProjectCompetency(Project.Project project, Competency.Competency competency)
        {
            Project = project;
            Competency = competency;
        }
    }
}