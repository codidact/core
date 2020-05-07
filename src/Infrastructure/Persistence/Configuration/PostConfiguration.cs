using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> entity)
        {
            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Body)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.NetVotes)
                .HasComputedColumnSql("(upvotes - downvotes)");

            entity.Property(e => e.Score)
                .HasColumnType("numeric");

            entity.Property(e => e.Title)
                .IsRequired();

            entity.HasOne(d => d.Category)
                .WithMany(p => p.Post)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_category_fk");

            entity.HasOne(d => d.CreatedByMember)
                .WithMany(p => p.PostCreatedByMember)
                .HasForeignKey(d => d.CreatedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_created_by_member_fk");

            entity.HasOne(d => d.LastModifiedByMember)
                .WithMany(p => p.PostLastModifiedByMember)
                .HasForeignKey(d => d.LastModifiedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_last_modified_by_member_fk");

            entity.HasOne(d => d.Member)
                .WithMany(p => p.PostMember)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_member_fk");

            entity.HasOne(d => d.ParentPost)
                .WithMany(p => p.InverseParentPost)
                .HasForeignKey(d => d.ParentPostId)
                .HasConstraintName("post_parent_post_fk");

            entity.HasOne(d => d.PostType)
                .WithMany(p => p.Post)
                .HasForeignKey(d => d.PostTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_post_type_fk");

        }
    }
}
