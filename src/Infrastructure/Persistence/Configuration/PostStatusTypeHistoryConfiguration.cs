using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class PostStatusTypeHistoryConfiguration : IEntityTypeConfiguration<PostStatusTypeHistory>
    {
        public void Configure(EntityTypeBuilder<PostStatusTypeHistory> entity)
        {
            entity.HasKey(e => e.HistoryId)
                .HasName("post_status_type_history_pk");

            entity.HasComment("For setting the status of a post locked/featured etc");

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
                .WithMany(p => p.PostStatusTypeHistory)
                .HasForeignKey(d => d.HistoryActivityMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_status_history_member_fk");

        }
    }
}
