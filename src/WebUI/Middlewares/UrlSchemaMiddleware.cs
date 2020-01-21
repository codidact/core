using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using Codidact.Domain.Extensions;

namespace Codidact.WebUI.Middlewares
{
    public class UrlSchemaMiddleware
    {
        private RequestDelegate _next;

        public UrlSchemaMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context,
            IOptionsSnapshot<CodidactOptions> options)
        {
            HttpRequest request = context.Request;

            if (options.Value.UseSubdomainSchema)
            {
                RewritePathToSubdomainSchema(request, options);
            }

            // Convert IP address to hostname and remove a possible port.
            request.Host = new HostString(options.Value.Hostname);

            RewriteCommunitySeperator(request, options);

            await _next(context);
        }

        private void RewritePathToSubdomainSchema(HttpRequest request,
            IOptionsSnapshot<CodidactOptions> options)
        {
            // Deal with naughty users that type the IP address directly.
            if (!request.Host.Host.EndsWith(options.Value.Hostname))
            {
                return;
            }

            string subdomains = request.Host.Host.TrimSuffix(options.Value.Hostname);
            string[] parts = subdomains.Split(".", StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 1)
            {
                request.Host = new HostString(options.Value.Hostname);
                request.Path = $"/community/{parts[0]}{request.Path}";
            }
            else if (parts.Length > 1)
            {
                request.Host = new HostString(options.Value.Hostname);
                request.Path = "/error/404";
            }
        }

        private void RewriteCommunitySeperator(HttpRequest request,
            IOptionsSnapshot<CodidactOptions> options)
        {
            if (request.Path.StartsWithSegments($"/{options.Value.CommunitySeparator}"))
            {
                request.Path = "/community" + request.Path.ToString().Substring($"/{options.Value.CommunitySeparator}".Length);
            }
        }
    }
}
