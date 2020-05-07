using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class CategoryHistoryConfiguration : IEntityTypeConfiguration<CategoryHistory>
    {
        public void Configure(EntityTypeBuilder<CategoryHistory> entity)
        {
            entity.HasKey(e => e.HistoryId)
                .HasName("category_history_pk");

            entity.Property(e => e.HistoryId)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Calculations)
                .HasDefaultValueSql("0");

            entity.Property(e => e.ContributesToTrustLevel)
                .IsRequired()
                .HasDefaultValueSql("true");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.DisplayName)
                .IsRequired();

            entity.Property(e => e.HistoryActivityAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.UrlPart)
                .HasMaxLength(20);

            entity.HasOne(d => d.HistoryActivityMember)
                .WithMany(p => p.CategoryHistory)
                .HasForeignKey(d => d.HistoryActivityMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("category_history_member_fk");

        }
    }
}
