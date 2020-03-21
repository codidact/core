using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class TrustLevelHistoryConfiguration : IEntityTypeConfiguration<TrustLevelHistory>
    {
        public void Configure(EntityTypeBuilder<TrustLevelHistory> entity)
        {
            entity.HasKey(e => e.HistoryId)
                .HasName("trust_level_history_pk");

            entity.HasComment("Name for each trust level and an explanation of each that a user should get when they get to that level.");

            entity.Property(e => e.HistoryId)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");


            entity.Property(e => e.DisplayName)
                .IsRequired();

            entity.Property(e => e.Explanation)
                .IsRequired();

            entity.Property(e => e.HistoryActivityAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.HasOne(d => d.HistoryActivityMember)
                .WithMany(p => p.TrustLevelHistory)
                .HasForeignKey(d => d.HistoryActivityMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("trust_level_history_member_fk");

        }
    }
}
