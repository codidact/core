using Codidact.Core.Application;
using Codidact.Core.Application.Common.Interfaces;
using Codidact.Core.Infrastructure;
using Codidact.Core.Infrastructure.Persistence;
using Codidact.Core.WebApp.Models;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;

namespace Codidact.Core.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddHttpContextAccessor();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "cookie";
                options.DefaultSignInScheme = "cookie";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("cookie")
            .AddOpenIdConnect("oidc", options =>
            {
                var identityOptions = Configuration.GetSection("Identity").Get<IdentityOptions>();
                options.Authority = identityOptions.Authority;
                options.RequireHttpsMetadata = identityOptions.RequireHttpsMetadata;
                options.ClientId = identityOptions.ClientId;
                options.ResponseType = identityOptions.ResponseType;
                options.ResponseMode = identityOptions.ResponseMode;
                options.CallbackPath = identityOptions.CallbackPath;
                options.SignedOutCallbackPath = identityOptions.SignedOutCallbackPath;
                options.SaveTokens = true;
                // Enable PKCE (authorization code flow only)
                options.UsePkce = true;
            });

            services.AddOptions<OpenIdConnectOptions>("oidc")
            .Configure<ISecretsService>((options, secretsService) =>
            {
                options.ClientSecret = secretsService.Get("Identity:ClientSecret").GetAwaiter().GetResult();
            }); 

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services
                .AddRazorPages()
                .AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // A change in .NET Core 3.0 prevents injection of ILogger anywhere but the Configure method.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

            if (env.EnvironmentName != "Test")
            {
                ApplyDatabaseMigrations(app, logger);
            }
        }

        // Applies database migrations; won't cause any changes if the database is up-to-date.
        private void ApplyDatabaseMigrations(IApplicationBuilder app, ILogger logger)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    try
                    {
                        context.Database.Migrate();
                    }
                    catch (System.Exception ex)
                    {
                        logger.LogError("Unable to apply database migrations. Check the connection string in your " +
                            "appsettings file.");
                        throw ex;
                    }
                }
            }
        }
    }
}
