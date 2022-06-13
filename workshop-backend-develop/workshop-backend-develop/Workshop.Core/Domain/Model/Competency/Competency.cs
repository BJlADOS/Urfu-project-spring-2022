using Workshop.Core.Domain.SharedKernel;

namespace Workshop.Core.Domain.Model.Competency
{
    public class Competency : Entity<long>, IAggregateRoot, ICompetency
    {
        public string Name { get; private set; }
        public CompetencyType CompetencyType { get; private set; }

        private Competency()
        {
        }

        public Competency(string name, CompetencyType type)
        {
            Name = name;
            CompetencyType = type;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateCompetencyType(CompetencyType competencyType)
        {
            CompetencyType = competencyType;
        }
    }
}