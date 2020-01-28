using Codidact.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
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
            foreach (var user in toCreateApplicationUsers)
            {
                var exists = await userManager.FindByNameAsync(user.UserName);
                if (exists == null)
                {

                    var randomPassword = GenerateRandomPassword();
                    var result = await userManager.CreateAsync(user, randomPassword);
                    if (result.Succeeded)
                    {
                        Console.WriteLine($"User created :{user.UserName}. Password: ${randomPassword}. Please change your password as soon as possible.");
                    }
                    else
                    {
                        Console.WriteLine($"Error Creating user:");
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"({error.Code}) {error.Description}");
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Generates a Random Password
        /// respecting the given strength requirements.
        /// </summary>
        /// <param name="opts">A valid PasswordOptions object
        /// containing the password strength requirements.</param>
        /// <returns>A random password</returns>
        private static string GenerateRandomPassword()
        {
            var opts = new PasswordOptions
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
        };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
