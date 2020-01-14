using Codidact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Infrastructure.Persistence.Configuration
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

            builder.Property(t => t.Email)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasIndex(t => t.Email)
                .IsUnique(true);

            builder.Property(t => t.IsFromStackExchange)
                .HasDefaultValue(false);

            builder.Property(t => t.IsSuspended)
                .HasDefaultValue(false);

        }
    }
}
