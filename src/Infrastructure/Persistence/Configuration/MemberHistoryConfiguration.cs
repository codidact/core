using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class MemberHistoryConfiguration : IEntityTypeConfiguration<MemberHistory>
    {
        public void Configure(EntityTypeBuilder<MemberHistory> entity)
        {
            entity.HasKey(e => e.HistoryId)
                .HasName("member_history_pk");

            entity.HasComment("This table will hold the global member records for a Codidact Instance. A member should only have one email to login with, that would be stored here. Does not include details such as password storage and hashing.");

            entity.Property(e => e.HistoryId)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.DisplayName)
                .IsRequired();

            entity.Property(e => e.HistoryActivityAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.IsSyncedWithNetworkAccount)
                .IsRequired()
                .HasDefaultValueSql("true");

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.NetworkAccountId)
                .HasComment("link to 'network_account' table?");

            entity.HasOne(d => d.HistoryActivityMember)
                .WithMany(p => p.MemberHistory)
                .HasForeignKey(d => d.HistoryActivityMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("member_history_member_fk");

        }
    }
}
