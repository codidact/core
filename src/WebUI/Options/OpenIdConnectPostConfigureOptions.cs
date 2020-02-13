using Codidact.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;

namespace Codidact.WebUI.Options
{
    public class OpenIdConnectPostConfigureOptions : IPostConfigureOptions<OpenIdConnectOptions>
    {
        private readonly ISecretsService _secretsService;

        public OpenIdConnectPostConfigureOptions(ISecretsService secretsService)
        {
            _secretsService = secretsService;
        }

        public async void PostConfigure(string name, OpenIdConnectOptions options)
        {
            options.ClientSecret = await _secretsService.Get("Identity:ClientSecret");
        }
    }
}
