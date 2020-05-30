using System;
using System.Collections.Generic;
using System.Linq;
using Codidact;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Net.Http.Headers;

namespace Codidact
{
	public class Startup
	{
		public static AppSettings _settings;
		public static IWebHostEnvironment _env;

		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			_env = env;

			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();

			if (env.IsDevelopment())
		{
				builder.AddUserSecrets<Startup>();
		}

			Configuration = builder.Build();
		}


		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<CodidactContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CodidactContext")));

			// Add ASP.Net Core identity
			services.AddIdentity<Users, Role>()
					.AddEntityFrameworkStores<CodidactContext>()
					.AddDefaultTokenProviders();

			services.Configure<IdentityOptions>(options =>
			{
				// Password settings
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 8;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;
				// Lockout settings
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
				options.Lockout.MaxFailedAccessAttempts = 5;
				// User settings
				options.User.RequireUniqueEmail = true;
			});

			services.AddMvc()
					.SetCompatibilityVersion(CompatibilityVersion.Latest)
					.AddRazorPagesOptions(options => {
						options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
						options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
					});

			// tweak Identity cookies
			services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Identity/Account/Login";
				options.LogoutPath = "/Identity/Account/Logout";
				options.AccessDeniedPath = "/Identity/Account/AccessDenied";
				//options.ExpireTimeSpan = TimeSpan.FromHours(12);
			});

			// add custom claims principal that includes User ID (as SID)
			services.AddScoped<IUserClaimsPrincipalFactory<Users>, Services.CustomClaimsPrincipalFactory>();

			//TODO : https://docs.microsoft.com/en-gb/aspnet/core/security/authentication/social/?view=aspnetcore-3.1&tabs=visual-studio
			/*			services.AddAuthentication()
								.AddMicrosoftAccount(microsoftOptions => { ... })
								.AddGoogle(googleOptions => { ... })
								.AddTwitter(twitterOptions => { ... })
								.AddFacebook(facebookOptions => { ... });*/

			services.Configure<CookiePolicyOptions>(options =>
			{
				options.CheckConsentNeeded = context => false;  // gdpr - is user consent for non-essential cookies needed?
				options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
			});

			// required for NLog
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			// read config into object for dep.inj.
			UsersConfig uuconfig = new UsersConfig();
			Configuration.GetSection("Users").Bind(uuconfig);
			services.AddSingleton(typeof(UsersConfig), uuconfig);

			services.Configure<KestrelServerOptions>(Configuration.GetSection("Kestrel"));

			AppSettings settings = new AppSettings();
			Configuration.GetSection("AppSettings").Bind(settings);
			Configuration.GetSection("EmailProvider").Bind(settings);
			settings.EmailFrom = $"{settings.EmailFrom} <{settings.EmailUser}>";
			Configuration.GetSection("Tokens").Bind(settings);
			services.AddSingleton(typeof(AppSettings), settings);

			// calculate values in settings
			settings.ImageRootPath = Path.Combine(_env.ContentRootPath, settings.ImageRoot);
			settings.ThumbRootPath = Path.Combine(_env.WebRootPath, settings.ThumbRoot);
			settings.AttachRootPath = Path.Combine(_env.WebRootPath, settings.AttachmentRoot);

			if (!Directory.Exists(settings.ImageRootPath))
				Directory.CreateDirectory(settings.ImageRootPath);
			if (!Directory.Exists(settings.ThumbRootPath))
				Directory.CreateDirectory(settings.ThumbRootPath);
			if (!Directory.Exists(settings.AttachmentRoot))
				Directory.CreateDirectory(settings.AttachmentRoot);

			// custom services
			services.AddSingleton<IEmailSender, Services.EmailSender>();


			services.AddResponseCaching();

			services.AddControllersWithViews().AddSessionStateTempDataProvider();
			services.AddRazorPages();

			// for reverse proxy setups, whitelist the proxy
			//services.Configure<ForwardedHeadersOptions>(options => { options.KnownProxies.Add(IPAddress.Parse("10.0.0.100")); });

			// tempdata in cookies, not session
			services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
			services.Configure<CookieTempDataProviderOptions>(options => {
				options.Cookie.IsEssential = true;
			});

			services.AddSignalR();

			// store all at-rest crypto keys in the filesystem so we can restart with the same ones
			services.AddDataProtection()
					.SetApplicationName("Codidact")
					.PersistKeysToFileSystem(new DirectoryInfo(settings.KeyStorageDirectory));

			// create authorisation policies
			services.AddAuthorization(options => {
				options.AddPolicy(AppConstants.PolicyAdminOnly, policy => policy.RequireRole(AppConstants.RoleAdmin));
				options.AddPolicy(AppConstants.PolicyModPlus, policy => policy.RequireRole(AppConstants.RoleAdmin, AppConstants.RoleModerator));
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

			// some XSS protection - note bans inline scripts
			app.Use(async (ctx, next) =>
			{
				ctx.Response.Headers.Add("Content-Security-Policy",
					"default-src 'self' cdnjs.cloudflare.com ajax.aspnetcdn.com code.jquery.com stackpath.bootstrapcdn.com unpkg.com " +
										"fonts.gstatic.com api.mapbox.com ; " +
					"img-src 'self' api.tiles.mapbox.com api.mapbox.com blob: data: ; " +
					"font-src 'self' fonts.googleapis.com fonts.gstatic.com cdnjs.cloudflare.com ; " +
					"style-src 'self' 'unsafe-inline' fonts.googleapis.com cdnjs.cloudflare.com ; " /*+
					"report-uri https://{_settings.csptoken}.report-uri.com/r/d/csp/enforce ;"*/);

				//ctx.Response.Headers.Add("X-Xss-Protection", "1");
				ctx.Response.Headers.Add("X-Frame-Options", "DENY" /*"SAMEORIGIN"*/);
				//ctx.Response.Headers.Add("X-Content-Type-Options", "nosniff");
				await next();
			});

			app.UseHttpsRedirection();
			app.UseStaticFiles(new StaticFileOptions()
			{
#if !DEBUG
				OnPrepareResponse = ctx => { 
					ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=" + 28800; // 8 hours for system stuff 
				}
#endif
			});

			app.UseCookiePolicy();
			app.UseResponseCaching();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapRazorPages();
			});
		}
	}
}
