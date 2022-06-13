using System;
using System.Collections.Generic;
using System.Text;

namespace Workshop.Core.Domain.Model.TeamReview
{
    public interface ITeamReview
    {
        long TeamId { get; }
        long ExpertId { get; }
    }
}