using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class PostTypeHistoryConfiguration : IEntityTypeConfiguration<PostTypeHistory>
    {
        public void Configure(EntityTypeBuilder<PostTypeHistory> entity)
        {
            entity.HasKey(e => e.HistoryId)
                .HasName("post_type_history_pk");

            entity.HasComment("Records the type of post, question/answer/blog etc");

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
                .WithMany(p => p.PostTypeHistory)
                .HasForeignKey(d => d.HistoryActivityMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_type_history_member_fk");

        }
    }
}
