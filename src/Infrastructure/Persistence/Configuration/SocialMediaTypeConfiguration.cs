using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class SocialMediaTypeConfiguration : IEntityTypeConfiguration<SocialMediaType>
    {
        public void Configure(EntityTypeBuilder<SocialMediaType> entity)
        {
            entity.HasComment("The types of social media that the member can display in their profile");

            entity.HasIndex(e => e.AccountUrl)
                .HasName("social_media_type_account_url_uc")
                .IsUnique();

            entity.HasIndex(e => e.DisplayName)
                .HasName("social_media_type_display_name_uc")
                .IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.AccountUrl)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.DisplayName)
                .IsRequired();

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.HasOne(d => d.CreatedByMember)
                .WithMany(p => p.SocialMediaTypeCreatedByMember)
                .HasForeignKey(d => d.CreatedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("social_media_created_by_member_fk");

            entity.HasOne(d => d.LastModifiedByMember)
                .WithMany(p => p.SocialMediaTypeLastModifiedByMember)
                .HasForeignKey(d => d.LastModifiedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("social_media_last_modified_by_member_fk");

        }
    }
}
