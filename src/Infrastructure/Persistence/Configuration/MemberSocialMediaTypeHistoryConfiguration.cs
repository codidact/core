using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class MemberSocialMediaTypeHistoryConfiguration : IEntityTypeConfiguration<MemberSocialMediaTypeHistory>
    {
        public void Configure(EntityTypeBuilder<MemberSocialMediaTypeHistory> entity)
        {
            entity.HasKey(e => e.HistoryId)
                .HasName("member_social_media_history_pk");

            entity.HasComment("The social media that the member would like to display in their community specific profile");

            entity.Property(e => e.HistoryId)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.HistoryActivityAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.HasOne(d => d.HistoryActivityMember)
                .WithMany(p => p.MemberSocialMediaTypeHistory)
                .HasForeignKey(d => d.HistoryActivityMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("member_social_media_type_history_member_fk");

        }
    }
}
