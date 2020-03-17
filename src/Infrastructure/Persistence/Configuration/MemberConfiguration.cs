﻿using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    /// <summary>
    /// Mapping configuration for the Member entity
    /// </summary>
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(t => t.DisplayName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.IsFromStackExchange)
                .HasDefaultValue(false);

            builder.Property(t => t.IsSuspended)
                .HasDefaultValue(false);

        }
    }
}
