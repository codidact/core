using Codidact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Infrastructure.Persistence.Configuration
{
    /// <summary>
    /// Mapping configuration for the Member entity
    /// </summary>
    public class TrustLevelCommunityConfiguration : IEntityTypeConfiguration<TrustLevelCommunity>
    {
        public void Configure(EntityTypeBuilder<TrustLevelCommunity> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(t => t.Name)
                .IsUnique();

            builder.HasIndex(t => t.Explanation)
                .IsUnique();

            builder.Property(t => t.IsSameAsInstance)
                .HasDefaultValue(true);
        }
    }
}
