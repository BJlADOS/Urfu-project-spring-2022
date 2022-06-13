using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop.Core.Domain.Model.Session;

namespace Workshop.Infrastructure.Storage.StorageMappings
{
    public class SessionTypeConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("Sessions");

            builder.Property<long>("Id");
            builder.HasKey("Id");

            builder.OwnsOne(x => x.Key, i => { i.Property(p => p.Id); });
        }
    }
}
