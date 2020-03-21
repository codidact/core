using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class MemberPrivilegeHistoryConfiguration : IEntityTypeConfiguration<MemberPrivilegeHistory>
    {
        public void Configure(EntityTypeBuilder<MemberPrivilegeHistory> entity)
        {
            entity.HasKey(e => e.HistoryId)
                .HasName("member_privilege_history_pk");

            entity.ToTable("member_privilege_history", "audit");

            entity.HasComment("For recording which members have which privilege in a community. If a member has a privilege suspended, then that is also recorded here, and a nightly task will auto undo the suspension once the privelege_suspension_end_date has passed.");

            entity.Property(e => e.HistoryId)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.HistoryActivityAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.HasOne(d => d.HistoryActivityMember)
                .WithMany(p => p.MemberPrivilegeHistory)
                .HasForeignKey(d => d.HistoryActivityMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("member_privilege_histry_member_fk");

        }
    }
}
