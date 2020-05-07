using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> entity)
        {
            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.HasOne(d => d.CreatedByMember)
                .WithMany(p => p.SettingCreatedByMember)
                .HasForeignKey(d => d.CreatedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("setting_created_by_member_fk");

            entity.HasOne(d => d.LastModifiedByMember)
                .WithMany(p => p.SettingLastModifiedByMember)
                .HasForeignKey(d => d.LastModifiedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("setting_last_modified_by_member_fk");

        }
    }
}
