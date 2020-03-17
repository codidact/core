using Codidact.Core.Application.Common.Interfaces;
using Codidact.Core.Domain.Entities;
using Codidact.Core.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Codidact.Core.WebApp.IntegrationTests
{
    /// <summary>
    /// This factory creates an .net Core server with the configuration provided
    /// for the purpose of testing against it
    /// </summary>
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .ConfigureServices(services =>
                {
                    // Remove the app's ApplicationDbContext registration.
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                            typeof(DbContextOptions<ApplicationDbContext>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    // Mock the ApplicationDbContext with the in memory db
                    // database for testing.
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                    });

                    services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

                    // Build the service provider.
                    var sp = services.BuildServiceProvider();

                    // Create a scope to obtain a reference to the database
                    // context (ApplicationDbContext).
                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<ApplicationDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    // Ensure the database is created.
                    context.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with test data.
                        InitializeDbForTests(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                })
                .UseEnvironment("Test");
        }

        public static void InitializeDbForTests(ApplicationDbContext context)
        {
            // Communities
            context.Communities.AddRange(new Community
            {
                Id = 1,
                Name = "Code"
            });

            // Members
            context.Members.AddRange(
                new Member { Id = 1, DisplayName = "John Doe" },
                new Member { Id = 2, DisplayName = "Haneen Kayle" },
                new Member { Id = 3, DisplayName = "Olaf Grechkovich" }
            );

            context.SaveChanges();
        }
    }
}
