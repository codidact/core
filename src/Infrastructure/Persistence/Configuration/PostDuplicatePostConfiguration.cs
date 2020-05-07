using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class PostDuplicatePostConfiguration : IEntityTypeConfiguration<PostDuplicatePost>
    {
        public void Configure(EntityTypeBuilder<PostDuplicatePost> entity)
        {
            entity.HasIndex(e => new { e.OriginalPostId, e.DuplicatePostId })
                .HasName("post_duplicate_post_original_post_duplicate_post_uc")
                .IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.HasOne(d => d.CreatedByMember)
                .WithMany(p => p.PostDuplicatePostCreatedByMember)
                .HasForeignKey(d => d.CreatedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_duplicate_post_created_by_member_fk");

            entity.HasOne(d => d.DuplicatePost)
                .WithMany(p => p.PostDuplicatePostDuplicatePost)
                .HasForeignKey(d => d.DuplicatePostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_duplicate_post_duplicate_post_fk");

            entity.HasOne(d => d.LastModifiedByMember)
                .WithMany(p => p.PostDuplicatePostLastModifiedByMember)
                .HasForeignKey(d => d.LastModifiedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_duplicate_post_last_modified_by_member_fk");

            entity.HasOne(d => d.OriginalPost)
                .WithMany(p => p.PostDuplicatePostOriginalPost)
                .HasForeignKey(d => d.OriginalPostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_duplicate_post_original_post_fk");

        }
    }
}
