using Codidact.Core.Domain.Entities;
using Codidact.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    public class CommunityConfiguration : IEntityTypeConfiguration<Community>
    {
        public void Configure(EntityTypeBuilder<Community> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(40)
                .IsRequired();

            builder.HasIndex(t => t.Name)
                .IsUnique(true);

            builder.Property(t => t.Tagline)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Url)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasIndex(t => t.Url)
               .IsUnique(true);

            builder.Property(t => t.Status)
                .HasDefaultValue(CommunityStatus.Active);
        }
    }
}
