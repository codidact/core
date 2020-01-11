using Microsoft.Extensions.DependencyInjection;
using System;

namespace Codidact.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services;
        }
    }
}
