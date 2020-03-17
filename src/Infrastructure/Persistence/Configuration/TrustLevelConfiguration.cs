using Codidact.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codidact.Core.Infrastructure.Persistence.Configuration
{
    /// <summary>
    /// Mapping configuration for the Member entity
    /// </summary>
    public class TrustLevelConfiguration : IEntityTypeConfiguration<TrustLevel>
    {
        public void Configure(EntityTypeBuilder<TrustLevel> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(t => t.Name)
                .IsUnique();

            builder.HasIndex(t => t.Explanation)
                .IsUnique();
        }
    }
}
