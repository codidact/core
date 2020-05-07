using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class MemberSocialMediaTypeConfiguration : IEntityTypeConfiguration<MemberSocialMediaType>
    {
        public void Configure(EntityTypeBuilder<MemberSocialMediaType> entity)
        {
            entity.HasComment("The social media that the member would like to display in their community specific profile");

            entity.HasIndex(e => new { e.MemberId, e.SocialMediaId })
                .HasName("member_social_media_social_media_member_uc")
                .IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.HasOne(d => d.CreatedByMember)
                .WithMany(p => p.MemberSocialMediaTypeCreatedByMember)
                .HasForeignKey(d => d.CreatedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("member_social_media_created_by_member_fk");

            entity.HasOne(d => d.LastModifiedByMember)
                .WithMany(p => p.MemberSocialMediaTypeLastModifiedByMember)
                .HasForeignKey(d => d.LastModifiedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("member_social_media_last_modified_by_member_fk");

            entity.HasOne(d => d.Member)
                .WithMany(p => p.MemberSocialMediaTypeMember)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("member_social_media_member_fk");

            entity.HasOne(d => d.SocialMedia)
                .WithMany(p => p.MemberSocialMediaType)
                .HasForeignKey(d => d.SocialMediaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("member_social_media_social_media_fk");

        }
    }
}
