using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class TrustLevelConfiguration : IEntityTypeConfiguration<TrustLevel>
    {
        public void Configure(EntityTypeBuilder<TrustLevel> entity)
        {
            entity.HasComment("Name for each trust level and an explanation of each that a user should get when they get to that level.");

            entity.HasIndex(e => e.DisplayName)
                .HasName("trust_level_display_name_uq")
                .IsUnique();

            entity.HasIndex(e => e.Explanation)
                .HasName("trust_level_explanation_uq")
                .IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.DisplayName)
                .IsRequired();

            entity.Property(e => e.Explanation)
                .IsRequired();

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.HasOne(d => d.CreatedByMember)
                .WithMany(p => p.TrustLevelCreatedByMember)
                .HasForeignKey(d => d.CreatedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by_member_fk");

            entity.HasOne(d => d.LastModifiedByMember)
                .WithMany(p => p.TrustLevelLastModifiedByMember)
                .HasForeignKey(d => d.LastModifiedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("last_modified_by_member_fk");

        }
    }
}
