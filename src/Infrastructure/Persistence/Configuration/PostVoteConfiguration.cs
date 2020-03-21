using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class PostVoteConfiguration : IEntityTypeConfiguration<PostVote>
    {
        public void Configure(EntityTypeBuilder<PostVote> entity)
        {
            entity.HasComment("The reason for this table is so that votes by spammers/serial voters can be undone.");

            entity.HasIndex(e => new { e.PostId, e.MemberId })
                .HasName("post_vote_post_member_uc")
                .IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.HasOne(d => d.CreatedByMember)
                .WithMany(p => p.PostVoteCreatedByMember)
                .HasForeignKey(d => d.CreatedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_vote_created_by_member_fk");

            entity.HasOne(d => d.LastModifiedByMember)
                .WithMany(p => p.PostVoteLastModifiedByMember)
                .HasForeignKey(d => d.LastModifiedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_vote_last_modified_by_member_fk");

            entity.HasOne(d => d.Member)
                .WithMany(p => p.PostVoteMember)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_vote_member_fk");

            entity.HasOne(d => d.Post)
                .WithMany(p => p.PostVote)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_vote_post_fk");

            entity.HasOne(d => d.VoteType)
                .WithMany(p => p.PostVote)
                .HasForeignKey(d => d.VoteTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_vote_vote_type_fk");

        }
    }
}
