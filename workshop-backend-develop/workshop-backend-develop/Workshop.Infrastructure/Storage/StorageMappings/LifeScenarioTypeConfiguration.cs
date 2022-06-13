using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop.Core.Domain.Model.LifeScenario;

namespace Workshop.Infrastructure.Storage.StorageMappings
{
    public class LifeScenarioTypeConfiguration :  IEntityTypeConfiguration<LifeScenario>
    {
        public void Configure(EntityTypeBuilder<LifeScenario> builder)
        {
            builder.ToTable("LifeScenarios");

            builder.HasKey(x => x.Key);
            builder.Property(x => x.Key).HasColumnName("Id");

            builder.Property("Name").IsRequired().HasMaxLength(400).IsRequired();
        }
    }
}
