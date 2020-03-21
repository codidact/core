using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class MemberPrivilegeConfiguration : IEntityTypeConfiguration<MemberPrivilege>
    {
        public void Configure(EntityTypeBuilder<MemberPrivilege> entity)
        {
            entity.HasComment("For recording which members have which privilege in a community. If a member has a privilege suspended, then that is also recorded here, and a nightly task will auto undo the suspension once the privelege_suspension_end_date has passed.");

            entity.HasIndex(e => new { e.MemberId, e.PrivilegeId })
                .HasName("member_privilege_member_privilege_uc")
                .IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.HasOne(d => d.CreatedByMember)
                .WithMany(p => p.MemberPrivilegeCreatedByMember)
                .HasForeignKey(d => d.CreatedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("member_privilege_created_by_member_fk");

            entity.HasOne(d => d.LastModifiedByMember)
                .WithMany(p => p.MemberPrivilegeLastModifiedByMember)
                .HasForeignKey(d => d.LastModifiedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("member_privilege_last_modified_by_member_fk");

            entity.HasOne(d => d.Member)
                .WithMany(p => p.MemberPrivilegeMember)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("member_privilege_member_fk");

            entity.HasOne(d => d.Privilege)
                .WithMany(p => p.MemberPrivilege)
                .HasForeignKey(d => d.PrivilegeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("member_privilege_privlege_fk");

        }
    }
}
