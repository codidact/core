using Codidact.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Codidact.Auth
{
    public static class SeedDatabase
    {
        /// <summary>
        /// Initializes identity data like users.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public async static Task InitializeIdentityData(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
           
            if (!userManager.Users.Any())
            {
                Console.WriteLine("No users found. Please create a new user.");
                await CreateInitialUser(userManager);
            }
        }

        /// <summary>
        /// Creates user with console input.
        /// </summary>
        /// <param name="userManager"></param>
        /// <returns></returns>
        private static async Task CreateInitialUser(UserManager<ApplicationUser> userManager)
        {
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();

            Console.WriteLine("Enter password:");
            string password = string.Empty;
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                password += keyInfo.KeyChar;
                keyInfo = Console.ReadKey(true);
            }

            IdentityResult result = await userManager.CreateAsync(
                new ApplicationUser
                {
                    Email = email,
                    UserName = username
                }, password);
            if (result.Succeeded)
            {
                Console.WriteLine($"User successfully created.");
            }
            else
            {
                Console.WriteLine($"Error Creating user:");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"({error.Code}) {error.Description}");
                }
                await CreateInitialUser(userManager);
            }
        }
    }
}
