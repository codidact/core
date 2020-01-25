using Codidact.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codidact.Auth
{
    public static class SeedDatabase
    {
        public async static Task InitializeIdentityUsers(this IApplicationBuilder app)
        {
            var toCreateApplicationUsers = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    UserName= "admin",
                    Email = "admin@codidact.com",
                }
            };
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
            foreach (var entity in toCreateApplicationUsers)
            {
                var exists = await userManager.FindByNameAsync(entity.UserName);
                if (exists == null)
                {
                    await userManager.CreateAsync(entity, "Aa123456!");
                }
            }
        }
    }
}
