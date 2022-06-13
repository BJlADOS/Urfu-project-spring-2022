using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop.Core.Domain.Model.Project;

namespace Workshop.Infrastructure.Storage.StorageMappings
{
    public class ProjectTypeConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");

            builder.HasKey(x => x.Key);
            builder.Property(x => x.Key).HasColumnName("Id");

            builder.Property("Name").IsRequired().HasMaxLength(400).IsRequired();

            builder.HasOne(x => x.KeyTechnology).WithMany(x => x.Projects)
                .HasForeignKey(p => p.KeyTechnologyId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.LifeScenario).WithMany(x => x.Projects)
                .HasForeignKey(p => p.LifeScenarioId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
