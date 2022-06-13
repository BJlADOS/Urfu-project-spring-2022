using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Core.Domain.Model.TeamCompetencyReview;

namespace Workshop.Infrastructure.Storage.StorageMappings
{
    public class TeamCompetencyReviewTypeConfiguration : IEntityTypeConfiguration<TeamCompetencyReview>
    {
        public void Configure(EntityTypeBuilder<TeamCompetencyReview> builder)
        {
            builder.ToTable("TeamCompetencyReviews");
            builder.HasKey(x => new { x.ExpertId, x.TeamId, x.CompetencyId });

            builder.Property(x => x.ExpertId).IsRequired();
            builder.Property(x => x.TeamId).IsRequired();
            builder.Property(x => x.CompetencyId).IsRequired();

            builder.HasOne(x => x.Expert).WithMany().HasForeignKey(x => x.ExpertId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Competency).WithMany().HasForeignKey(x => x.CompetencyId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Team).WithMany(t => t.TeamCompetencyReviews).HasForeignKey(x => x.TeamId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
