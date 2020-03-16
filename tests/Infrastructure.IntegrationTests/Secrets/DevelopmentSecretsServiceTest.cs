using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Codidact.Infrastructure.IntegrationTests.Secrets
{
    public class DevelopmentSecretsServiceTest
    {
        private readonly DevelopmentSecretsService _devSecretService;

        private readonly Dictionary<string, string> InMemoryConfiguration =
            new Dictionary<string, string>
            {
                { "DefaultConnection", "LocalDb" },
            };

        public DevelopmentSecretsServiceTest()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(InMemoryConfiguration).Build();
            _devSecretService = new DevelopmentSecretsService(config);
        }

        [Fact]
        public async Task GetSValueFromDevSecrets()
        {
            var connection = await _devSecretService.Get(InMemoryConfiguration.Keys.First());
            Assert.Equal(connection, InMemoryConfiguration.Values.First());
        }
    }
}
