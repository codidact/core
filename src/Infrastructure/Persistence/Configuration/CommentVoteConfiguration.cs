using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class CommentVoteConfiguration : IEntityTypeConfiguration<CommentVote>
    {
        public void Configure(EntityTypeBuilder<CommentVote> entity)
        {
            entity.HasIndex(e => new { e.CommentId, e.MemberId })
                .HasName("comment_vote_comment_member_uc")
                .IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn();


            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");


            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.HasOne(d => d.Comment)
                .WithMany(p => p.CommentVote)
                .HasForeignKey(d => d.CommentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("commentvote_comment_fk");

            entity.HasOne(d => d.CreatedByMember)
                .WithMany(p => p.CommentVoteCreatedByMember)
                .HasForeignKey(d => d.CreatedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_vote_created_by_member_fk");

            entity.HasOne(d => d.LastModifiedByMember)
                .WithMany(p => p.CommentVoteLastModifiedByMember)
                .HasForeignKey(d => d.LastModifiedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_vote_last_modified_by_member_fk");

            entity.HasOne(d => d.Member)
                .WithMany(p => p.CommentVoteMember)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_vote_member_fk");

            entity.HasOne(d => d.VoteType)
                .WithMany(p => p.CommentVote)
                .HasForeignKey(d => d.VoteTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_vote_vote_type_fk");

        }
    }
}
