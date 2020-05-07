using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class CommentHistoryConfiguration : IEntityTypeConfiguration<CommentHistory>
    {
        public void Configure(EntityTypeBuilder<CommentHistory> entity)
        {
            entity.HasKey(e => e.HistoryId)
                .HasName("comment_history_pk");

            entity.HasComment("Table for the comments on posts, both questions and answers.");

            entity.Property(e => e.HistoryId)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Body)
                .IsRequired()
                .HasColumnName("body");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.NetVotes)
                .HasComputedColumnSql("(upvotes - downvotes)");

            entity.Property(e => e.Score)
                .HasColumnType("numeric");

            entity.HasOne(d => d.HistoryActivityMember)
                .WithMany(p => p.CommentHistory)
                .HasForeignKey(d => d.HistoryActivityMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_history_member_fk");

        }
    }
}
