using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class SocialMediaTypeHistoryConfiguration : IEntityTypeConfiguration<SocialMediaTypeHistory>
    {
        public void Configure(EntityTypeBuilder<SocialMediaTypeHistory> entity)
        {
            entity.HasKey(e => e.HistoryId)
                .HasName("social_media_type_history_pk");

            entity.HasComment("The types of social media that the member can display in their profile");

            entity.Property(e => e.HistoryId)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.AccountUrl)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.DisplayName)
                .IsRequired();

            entity.Property(e => e.HistoryActivityAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.HasOne(d => d.HistoryActivityMember)
                .WithMany(p => p.SocialMediaTypeHistory)
                .HasForeignKey(d => d.HistoryActivityMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("social_media_type_history_member_fk");

        }
    }
}
