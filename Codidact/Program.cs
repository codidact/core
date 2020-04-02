using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Codidact;
using FastExpressionCompiler;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Codidact
{
	public class Program
	{
		public static void Main(string[] args)
		{
			// NLog: setup the logger first to catch all errors
			var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
			try
			{
				logger.Warn("init main");

				var host = CreateHostBuilder(args)
					.Build();

				// cache defaults, ensure admin user exists
				using (var scope = host.Services.CreateScope())
				{
					var services = scope.ServiceProvider;
					var context = services.GetRequiredService<CodidactContext>();

#if DEBUG
					// code first auto-migration of the DB
					context.Database.Migrate();
#endif
					// create admin user if it doesn't exist, and all roles
					CreateUserAndRolesAsync(services, context).Wait();
				}

				// mapster to use FEC
				TypeAdapterConfig.GlobalSettings.Compiler = exp => exp.CompileFast();

				if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
				{
					string err = ExecCmd($"rm -f {AppConstants.KestrelSocket}");
					if (!string.IsNullOrEmpty(err))
						logger.Error("Failed to delete socket: " + err);

					host.Start();

					err = ExecCmd($"chmod go+w {AppConstants.KestrelSocket}");
					if (!string.IsNullOrEmpty(err))
						logger.Error("Failed to chmod socket: " + err);
				}
				else
					host.Start();

				host.WaitForShutdown();

				logger.Warn("exit main");
			}
			catch (Exception ex)
			{
				logger.Error(ex, "Stopped program because of exception");
				throw;
			}
			finally
			{
				// Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
				NLog.LogManager.Flush();
				NLog.LogManager.Shutdown();
			}
		}


		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureServices((context, services) => {
					services.Configure<KestrelServerOptions>(context.Configuration.GetSection("Kestrel"));
				})
				.ConfigureWebHostDefaults(webBuilder =>
				{
					if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
					{
						webBuilder.ConfigureKestrel(opts => { opts.ListenUnixSocket(AppConstants.KestrelSocket); });
					}

					webBuilder.UseStartup<Startup>();
				})
				.ConfigureLogging(logging =>
				{
					logging.ClearProviders();
					logging.SetMinimumLevel(LogLevel.Warning);
				})
				.UseNLog();




		private static string ExecCmd(string cmd)
		{
			var escapedArgs = cmd.Replace("\"", "\\\"");

			var process = new Process()
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "/bin/bash",
					Arguments = $"-c \"{escapedArgs}\"",
					RedirectStandardOutput = false,
					RedirectStandardError = true,
					UseShellExecute = false,
					CreateNoWindow = true,
				}
			};
			process.Start();
			string result = process.StandardError.ReadToEnd();
			process.WaitForExit();
			return result;
		}


		// helper method to initialise DB with users and roles
		private static async Task CreateUserAndRolesAsync(IServiceProvider services, CodidactContext context)
		{
			var UserManager = services.GetRequiredService<UserManager<Users>>();
			var RoleManager = services.GetRequiredService<RoleManager<Role>>();
			var usersConfig = services.GetService<UsersConfig>();

			// create the admin user, if one doesn't already exist.
			var _user = await UserManager.FindByEmailAsync(usersConfig.AdminEmail);
			if (_user == null)
			{
				try
				{
					context.Database.BeginTransaction();

					// create roles
					IdentityResult roleResult;
					foreach (var roleName in AppConstants.RoleNames)
					{
						var roleExist = await RoleManager.RoleExistsAsync(roleName);

						if (!roleExist)
							roleResult = await RoleManager.CreateAsync(new Role { Name = roleName, NormalizedName = roleName.ToUpper() });
					}

					var admin = new Users
					{
						UserName = usersConfig.AdminName,
						DisplayName = usersConfig.AdminName,
						Email = usersConfig.AdminEmail,
						EmailConfirmed = true,
						CreationDate = DateTime.Now,
						LockoutEnabled = false,
						AccessFailedCount = 0
					};

					// create admin and add add roles
					var createAdminUser = await UserManager.CreateAsync(admin, usersConfig.AdminPassword);
					if (createAdminUser.Succeeded)
					{
						await UserManager.AddToRoleAsync(admin, AppConstants.RoleAdmin);
						await UserManager.AddToRoleAsync(admin, AppConstants.RoleModerator);
						await UserManager.AddToRoleAsync(admin, AppConstants.RoleTrusted);
					}

					context.SaveChanges();

					context.Database.CommitTransaction();
				}
				catch (Exception e)
				{
					context.Database.RollbackTransaction();
					throw e;
				}
			}
		}
	}
}
