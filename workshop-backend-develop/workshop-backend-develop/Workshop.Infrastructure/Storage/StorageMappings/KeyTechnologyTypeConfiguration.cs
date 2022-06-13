using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop.Core.Domain.Model.KeyTechnology;

namespace Workshop.Infrastructure.Storage.StorageMappings
{
    public class KeyTechnologyTypeConfiguration : IEntityTypeConfiguration<KeyTechnology>
    {
        public void Configure(EntityTypeBuilder<KeyTechnology> builder)
        {
            builder.ToTable("KeyTechnologies");

            builder.HasKey(x => x.Key);
            builder.Property(x => x.Key).HasColumnName("Id");

            builder.Property("Name").IsRequired().HasMaxLength(400).IsRequired();
        }
    }
}
