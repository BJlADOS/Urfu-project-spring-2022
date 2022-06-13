using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop.Core.Domain.Model.Competency;

namespace Workshop.Infrastructure.Storage.StorageMappings
{
    public class CompetencyTypeConfiguration : IEntityTypeConfiguration<Competency>
    {
        public void Configure(EntityTypeBuilder<Competency> builder)
        {
            builder.ToTable("Competencies");

            builder.Property(x => x.CompetencyType)
                .HasConversion<string>();

            builder.HasKey(x => x.Key);
            builder.Property(x => x.Key).HasColumnName("Id");

            builder.Property("Name").IsRequired().HasMaxLength(400).IsRequired();
        }
    }
}
 