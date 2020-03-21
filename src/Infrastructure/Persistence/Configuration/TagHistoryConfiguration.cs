using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class TagHistoryLevelConfiguration : IEntityTypeConfiguration<TagHistory>
    {
        public void Configure(EntityTypeBuilder<TagHistory> entity)
        {
            entity.HasKey(e => e.HistoryId)
                .HasName("tag_history_pk");

            entity.HasComment("Table for all of the tags - history");

            entity.Property(e => e.HistoryId)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Body)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.HistoryActivityAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("true");

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.HasOne(d => d.HistoryMember)
                .WithMany(p => p.TagHistory)
                .HasForeignKey(d => d.HistoryMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tag_history_member_fk");

        }
    }
}
