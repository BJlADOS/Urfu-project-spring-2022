using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Core.Domain.Model.TeamReview;

namespace Workshop.Infrastructure.Storage.StorageMappings
{
    public class TeamReviewTypeConfiguration : IEntityTypeConfiguration<TeamReview>
    {
        public void Configure(EntityTypeBuilder<TeamReview> builder)
        {
            builder.ToTable("TeamReviews");
            builder.HasKey(x => new { x.ExpertId, x.TeamId });

            builder.Property(x => x.ExpertId).IsRequired();
            builder.Property(x => x.TeamId).IsRequired();

            builder.HasOne(x => x.Expert).WithMany().HasForeignKey(x => x.ExpertId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Team).WithMany(t => t.TeamReviews).HasForeignKey(x => x.TeamId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
