using Workshop.Core.Domain.SharedKernel;

namespace Workshop.Core.Domain.Model.Role
{
    public class Role : Entity<long>, IAggregateRoot, IRole
    {
        public string Name { get; private set; }
        public long ProjectId { get; private set; }
        public Project.Project Project { get; }

        public Role()
        {
        }

        public Role(string name, Project.Project project)
        {
            Name = name;
            Project = project;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }
    }
}