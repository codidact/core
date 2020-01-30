using Codidact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Infrastructure.Persistence.Configuration
{
    /// <summary>
    /// Mapping configuration for the Member entity
    /// </summary>
    public class MemberCommunityConfiguration : IEntityTypeConfiguration<MemberCommunity>
    {
        public void Configure(EntityTypeBuilder<MemberCommunity> builder)
        {
            builder.Property(t => t.DisplayName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Email)
                .HasMaxLength(320)
                .IsRequired();

            builder.HasIndex(t => t.Email)
                .IsUnique(true);

            builder.Property(t => t.IsFromStackExchange)
                .HasDefaultValue(false);

            builder.Property(t => t.IsSuspended)
                .HasDefaultValue(false);

            builder.Property(t => t.IsSameAsInstance)
               .HasDefaultValue(true);

            builder.HasIndex(t => new { t.MemberId, t.CommunityId })
                .IsUnique();

        }
    }
}
