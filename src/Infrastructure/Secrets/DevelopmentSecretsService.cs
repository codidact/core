
using Codidact.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Codidact.Infrastructure
{
    /// <summary>
    /// Developement secrets service that uses appsettings
    /// or local dotnet secrets to get the secrets
    /// </summary>
    public class DevelopmentSecretsService : ISecretsService
    {
        private readonly IConfiguration _configuration;

        public DevelopmentSecretsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> Get(string key)
        {
            return Task.FromResult(_configuration.GetValue<string>(key));
        }
    }
}
