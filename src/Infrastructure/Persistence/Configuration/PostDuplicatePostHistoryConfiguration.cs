using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class PostDuplicatePostHistoryConfiguration : IEntityTypeConfiguration<PostDuplicatePostHistory>
    {
        public void Configure(EntityTypeBuilder<PostDuplicatePostHistory> entity)
        {
            entity.HasKey(e => e.HistoryId)
                .HasName("post_duplicate_post_history_pk");

            entity.Property(e => e.HistoryId)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.HistoryActivityAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.HasOne(d => d.HistoryActivityMember)
                .WithMany(p => p.PostDuplicatePostHistory)
                .HasForeignKey(d => d.HistoryActivityMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_duplicate_post_history_member_fk");

        }
    }
}
