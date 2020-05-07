using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class VoteTypeHistoryConfiguration : IEntityTypeConfiguration<VoteTypeHistory>
    {
        public void Configure(EntityTypeBuilder<VoteTypeHistory> entity)
        {
            entity.HasKey(e => e.HistoryId)
                .HasName("vote_type_history_pk");

            entity.HasComment("Table for the vote types, upvote/downvote.");

            entity.Property(e => e.HistoryId)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.DisplayName)
                .IsRequired();

            entity.Property(e => e.HistoryActivityAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.HasOne(d => d.HistoryActivityMember)
                .WithMany(p => p.VoteTypeHistory)
                .HasForeignKey(d => d.HistoryActivityMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vote_type_history_member_fk");

        }
    }
}
