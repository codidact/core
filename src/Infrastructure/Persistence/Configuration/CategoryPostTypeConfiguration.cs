using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class CategoryPostTypeConfiguration : IEntityTypeConfiguration<CategoryPostType>
    {
        public void Configure(EntityTypeBuilder<CategoryPostType> entity)
        {
            entity.HasComment("CategoryPostType");

            entity.HasIndex(e => new { e.CategoryId, e.PostTypeId })
                .HasName("category_post_type_category_post_type_uc")
                .IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");


            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("true");

            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()");

            entity.HasOne(d => d.CreatedByMember)
                .WithMany(p => p.CategoryPostTypeCreatedByMember)
                .HasForeignKey(d => d.CreatedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("category_post_type_created_by_member_fk");

            entity.HasOne(d => d.LastModifiedByMember)
                .WithMany(p => p.CategoryPostTypeLastModifiedByMember)
                .HasForeignKey(d => d.LastModifiedByMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("category_post_type_last_modified_by_member_fk");

        }
    }
}
