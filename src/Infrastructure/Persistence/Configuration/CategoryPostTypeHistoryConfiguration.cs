using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class CategoryPostTypeHistoryConfiguration : IEntityTypeConfiguration<CategoryPostTypeHistory>
    {
        public void Configure(EntityTypeBuilder<CategoryPostTypeHistory> entity)
        {
            entity.HasKey(e => e.HistoryId)
                .HasName("category_post_type_history_pk");

            entity.HasComment("CategoryPostType");

            entity.Property(e => e.HistoryId)
                .HasColumnName("history_id")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CategoryId).HasColumnName("category_id");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()");

            entity.Property(e => e.CreatedByMemberId).HasColumnName("created_by_member_id");

            entity.Property(e => e.HistoryActivityAt)
                .HasColumnName("history_activity_at")
                .HasDefaultValueSql("now()");

            entity.Property(e => e.HistoryActivityMemberId).HasColumnName("history_activity_member_id");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasColumnName("is_active")
                .HasDefaultValueSql("true");

            entity.Property(e => e.LastModifiedAt)
                .HasColumnName("last_modified_at")
                .HasDefaultValueSql("now()");

            entity.Property(e => e.LastModifiedByMemberId).HasColumnName("last_modified_by_member_id");

            entity.Property(e => e.PostTypeId).HasColumnName("post_type_id");

            entity.HasOne(d => d.HistoryActivityMember)
                .WithMany(p => p.CategoryPostTypeHistory)
                .HasForeignKey(d => d.HistoryActivityMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("category_post_type_history_member_fk");

        }
    }
}
