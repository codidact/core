using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class PostHistoryConfiguration : IEntityTypeConfiguration<PostHistory>
    {
        public void Configure(EntityTypeBuilder<PostHistory> entity)
        {
            entity.HasKey(e => e.HistoryId)
                .HasName("post_history_pk");

            entity.Property(e => e.HistoryId)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Body)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.HistoryActivityAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.NetVotes)
                .HasComputedColumnSql("(upvotes - downvotes)");

            entity.Property(e => e.Score)
                .HasColumnType("numeric");

            entity.Property(e => e.Title)
                .IsRequired();

            entity.Property(e => e.Upvotes).HasColumnName("upvotes");

            entity.Property(e => e.Views).HasColumnName("views");

            entity.HasOne(d => d.HistoryActivityMember)
                .WithMany(p => p.PostHistory)
                .HasForeignKey(d => d.HistoryActivityMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_history_member_fk");

        }
    }
}
