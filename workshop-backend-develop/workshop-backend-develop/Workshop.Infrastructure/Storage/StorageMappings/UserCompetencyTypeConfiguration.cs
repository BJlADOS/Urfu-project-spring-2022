using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop.Core.Domain.Model.UserCompetency;

namespace Workshop.Infrastructure.Storage.StorageMappings
{
    public class UserCompetencyTypeConfiguration : IEntityTypeConfiguration<UserCompetency>
    {
        public void Configure(EntityTypeBuilder<UserCompetency> builder)
        {
            builder.ToTable("UserCompetencies");
            builder.HasKey(x => new { x.CompetencyId, x.UserId });

            builder.Property(x => x.UserCompetencyType)
                .HasConversion<string>();

            builder.Property(x => x.CompetencyId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();

            builder.HasOne(x => x.User).WithMany(x => x.Competencies).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Competency).WithMany().HasForeignKey(x => x.CompetencyId).OnDelete(DeleteBehavior.Cascade);
        }
    }

}
