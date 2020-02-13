using Codidact.Application.Common.Interfaces;
using Codidact.Infrastructure.Identity;
using Codidact.Infrastructure.Persistence;
using Codidact.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Codidact.Infrastructure
{
    /// <summary>
    /// Dependency Injection module for the infrastructure
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds all of the application services into the service collection
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(async (provider, options) =>
            {
                var secretsService = provider.GetRequiredService<ISecretsService>();
                var connectionString = await secretsService.Get("ConnectionStrings:DefaultConnection");
                options.UseNpgsql(connectionString,
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddScoped<ISettingsService, SettingsService>();

            // TODO: Implement a Service for production
            services.AddSingleton<ISecretsService, DevelopmentSecretsService>();

            return services;
        }
    }
}
