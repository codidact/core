using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> entity)
        {
            entity.HasComment("Table for the comments on posts, both questions and answers.");

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

            entity.Property(e => e.Upvotes).HasColumnName("upvotes");

            entity.HasOne(d => d.CreatedByMember)
                .WithMany(p => p.CommentCreatedByMember)
                .HasForeignKey(d => d.CreatedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_created_by_member_fk");

            entity.HasOne(d => d.LastModifiedByMember)
                .WithMany(p => p.CommentLastModifiedByMember)
                .HasForeignKey(d => d.LastModifiedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_last_modified_by_member_fk");

            entity.HasOne(d => d.Member)
                .WithMany(p => p.CommentMember)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_member_fk");

            entity.HasOne(d => d.ParentComment)
                .WithMany(p => p.InverseParentComment)
                .HasForeignKey(d => d.ParentCommentId)
                .HasConstraintName("comment_parent_comment_fk");

            entity.HasOne(d => d.Post)
                .WithMany(p => p.Comment)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_post_fk");

        }
    }
}
