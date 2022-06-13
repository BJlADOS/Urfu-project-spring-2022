using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop.Core.Domain.Model.Team;

namespace Workshop.Infrastructure.Storage.StorageMappings
{
    public class TeamTypeConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Teams");

            builder.Property(x => x.TeamStatus)
                .HasConversion<string>();

            builder.HasKey(x => x.Key);
            builder.Property(x => x.Key).HasColumnName("Id");

            builder.HasMany(x => x.Users).WithOne(x => x.Team).HasForeignKey(x => x.TeamId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Expert).WithMany().HasForeignKey(x => x.ExpertId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Auditorium).WithMany().HasForeignKey(x => x.AuditoriumId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.Project).WithMany().HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.TeamReviews).WithOne(x => x.Team).HasForeignKey(x => x.TeamId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.TeamCompetencyReviews).WithOne(x => x.Team).HasForeignKey(x => x.TeamId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
