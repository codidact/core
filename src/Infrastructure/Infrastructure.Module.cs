using Codidact.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Codidact.Infrastructure
{
    /// <summary>
    /// Dependency Injection module for the infrastructure
    /// </summary>
    public static class InfrastructureModule
    {
        /// <summary>
        /// Adds all of the application services into the service collection
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseNpgsql(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            return services;
        }
    }
}
