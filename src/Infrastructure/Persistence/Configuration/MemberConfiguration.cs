using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> entity)
        {
            entity.HasComment("This table will hold the global member records for a Codidact Instance. A member should only have one email to login with, that would be stored here. Does not include details such as password storage and hashing.");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.DisplayName)
                .IsRequired();

            entity.Property(e => e.IsSyncedWithNetworkAccount)
                .IsRequired()
                .HasDefaultValueSql("true");

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.NetworkAccountId)
                .HasComment("link to 'network_account' table?");

            entity.HasOne(d => d.CreatedByMember)
                .WithMany(p => p.InverseCreatedByMember)
                .HasForeignKey(d => d.CreatedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("member_created_by_member_fk");

            entity.HasOne(d => d.LastModifiedByMember)
                .WithMany(p => p.InverseLastModifiedByMember)
                .HasForeignKey(d => d.LastModifiedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("member_last_modified_by_member_fk");

            entity.HasOne(d => d.TrustLevel)
                .WithMany(p => p.Member)
                .HasForeignKey(d => d.TrustLevelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("member_trust_level_fk");

        }
    }
}
