using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop.Core.Domain.Model.Event;

namespace Workshop.Infrastructure.Storage.StorageMappings
{
    public class EventTypeConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(x => x.Key);
            builder.Property(x => x.Key).HasColumnName("Id");
        }
    }
}
