using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Codidact.Application;
using Codidact.Infrastructure;
using Codidact.Infrastructure.Persistence;
using Codidact.Infrastructure.Common;

namespace Codidact.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services.Configure<CodidactOptions>(Configuration.GetSection("Codidact"));
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            ApplyDatabaseMigrations(app, logger);
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
                        logger.LogError("Unable to apply database migrations. Check the connection string in your appsettings file.");
                        throw ex;
                    }
                }
            }
        }
    }
}
