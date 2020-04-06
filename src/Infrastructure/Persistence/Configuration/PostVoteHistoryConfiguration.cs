using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class PostVoteHistoryConfiguration : IEntityTypeConfiguration<PostVoteHistory>
    {
        public void Configure(EntityTypeBuilder<PostVoteHistory> entity)
        {
            entity.HasKey(e => e.HistoryId)
                .HasName("post_vote_history_pk");

            entity.HasComment("The reason for this table is so that votes by spammers/serial voters can be undone.");

            entity.Property(e => e.HistoryId)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.HistoryActivityAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.HasOne(d => d.HistoryActivityMember)
                .WithMany(p => p.PostVoteHistory)
                .HasForeignKey(d => d.HistoryActivityMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_vote_history_member_fk");

        }
    }
}
