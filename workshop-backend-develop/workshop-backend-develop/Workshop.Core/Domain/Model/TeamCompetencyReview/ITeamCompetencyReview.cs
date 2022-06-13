using System;
using System.Collections.Generic;
using System.Text;

namespace Workshop.Core.Domain.Model.TeamCompetencyReview
{
    public interface ITeamCompetencyReview
    {
        long TeamId { get; }
        long ExpertId { get; }
        long CompetencyId { get; }
    }
}