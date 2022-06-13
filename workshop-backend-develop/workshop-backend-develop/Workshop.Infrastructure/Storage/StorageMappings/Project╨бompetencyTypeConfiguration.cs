using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop.Core.Domain.Model.ProjectCompetency;
using Workshop.Core.Domain.Model.UserCompetency;

namespace Workshop.Infrastructure.Storage.StorageMappings
{
    public class ProjectСompetencyTypeConfiguration : IEntityTypeConfiguration<ProjectCompetency>
    {
        public void Configure(EntityTypeBuilder<ProjectCompetency> builder)
        {
            builder.ToTable("ProjectCompetencies");
            builder.HasKey(x => new { x.CompetencyId, x.ProjectId });

            builder.Property(x => x.CompetencyId).IsRequired();
            builder.Property(x => x.ProjectId).IsRequired();

            builder.HasOne(x => x.Project).WithMany(x => x.Competencies).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Competency).WithMany().HasForeignKey(x => x.CompetencyId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
