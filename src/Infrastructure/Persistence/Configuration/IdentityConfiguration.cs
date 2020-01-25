using Codidact.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Codidact.Infrastructure.Persistence.Configuration
{
    public static class IdentityConfiguration
    {
        public static void ConfigureIdentity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().ToTable("users");
            modelBuilder.Entity<IdentityRole<long>>().ToTable("roles");
            modelBuilder.Entity<IdentityUserToken<long>>().ToTable("user_tokens");
            modelBuilder.Entity<IdentityUserClaim<long>>().ToTable("user_claims");
            modelBuilder.Entity<IdentityUserLogin<long>>().ToTable("user_logins");
            modelBuilder.Entity<IdentityRoleClaim<long>>().ToTable("role_claims");
            modelBuilder.Entity<IdentityUserRole<long>>().ToTable("user_roles");

            modelBuilder.Entity<ApplicationUser>().Property(t => t.Email)
                .HasMaxLength(320)
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .Ignore(t => t.PhoneNumber)
                .Ignore(t => t.PhoneNumberConfirmed);

        }
    }
}
