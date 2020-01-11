using Codidact.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Codidact.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddInfrastructure();
            return services;
        }

    }
}
