using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Calculations)
                .HasDefaultValueSql("0");

            entity.Property(e => e.ContributesToTrustLevel)
                .IsRequired()
                .HasDefaultValueSql("true");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.DisplayName)
                .IsRequired();

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.UrlPart)
                .HasMaxLength(20);

            entity.HasOne(d => d.CreatedByMember)
                .WithMany(p => p.CategoryCreatedByMember)
                .HasForeignKey(d => d.CreatedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("category_created_by_member_fk");

            entity.HasOne(d => d.LastModifiedByMember)
                .WithMany(p => p.CategoryLastModifiedByMember)
                .HasForeignKey(d => d.LastModifiedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("category_last_modified_by_member_fk");

            entity.HasOne(d => d.ParticipationMinimumTrustLevel)
                .WithMany(p => p.Category)
                .HasForeignKey(d => d.ParticipationMinimumTrustLevelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("category_participation_minimum_trust_level_fk");

        }
    }
}
