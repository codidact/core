using Codidact.Core.Application;
using Codidact.Core.Application.Common.Interfaces;
using Codidact.Core.Domain.Entities;
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
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

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

            app.UseEndpoints(endpoints => endpoints.MapRazorPages());

            if (env.EnvironmentName != "Test")
            {
                ApplyDatabaseMigrations(app, logger);
                if (env.IsDevelopment())
                {
                    SeedDatabase(app, logger);
                }
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

        private void SeedDatabase(IApplicationBuilder app, ILogger logger)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    try
                    {
                        TrustLevel adminTrustLevel;
                        if (!context.TrustLevels.Any())
                        {
                            logger.LogDebug("Adding Trust Levels...");

                            adminTrustLevel = new TrustLevel
                            {
                                DisplayName = "Instance Administrator",
                                Explanation = "Administrator Level Trust",
                            };
                            context.TrustLevels.Add(adminTrustLevel);
                            context.SaveChanges();
                        }
                        else
                        {
                            adminTrustLevel = context.TrustLevels
                                .OrderByDescending(trust => trust.CreatedAt)
                                .FirstOrDefault();
                        }

                        Category category;
                        if (!context.Categories.Any())
                        {
                            logger.LogDebug("Creating main category...");

                            category = new Category
                            {
                                DisplayName = "main",
                                ParticipationMinimumTrustLevelId = adminTrustLevel.Id,
                                ShortExplanation = "Codidact Questions",
                                LongExplanation = "Main Category for Codidact where all the codidact related questions go to"
                            };
                            context.Categories.Add(category);
                            context.SaveChanges();
                        }
                        else
                        {
                            category = context.Categories
                                .OrderByDescending(category => category.CreatedAt)
                                .FirstOrDefault();
                        }

                        Member communityMember;
                        // members
                        if (!context.Members.Any())
                        {
                            logger.LogDebug("Creating community member...");

                            communityMember = new Member
                            {
                                DisplayName = "Codidact Community",
                                Bio = "The codidact community manifested",
                                TrustLevelId = adminTrustLevel.Id
                            };
                            context.Members.Add(communityMember);
                            context.SaveChanges();
                        }
                        else
                        {
                            communityMember = context.Members
                                .OrderByDescending(member => member.CreatedAt)
                                .FirstOrDefault();
                        }

                        // post types
                        if (!context.PostTypes.Any())
                        {
                            logger.LogDebug("Creating post types...");

                            context.PostTypes.Add(new PostType
                            {
                                Id = Domain.Enums.PostType.Question,
                                DisplayName = "Question",
                                Description = "A question"
                            });
                            context.SaveChanges();
                        }
                        else
                        {
                            communityMember = context.Members
                                .OrderByDescending(member => member.CreatedAt)
                                .FirstOrDefault();
                        }

                        // questions
                        if (!context.Posts.Any(post => post.PostTypeId == Domain.Enums.PostType.Question))
                        {
                            logger.LogDebug("Creating a few sample questions...");

                            context.Posts.Add(new Post
                            {
                                Title = "What is Codidact?",
                                Body = "I don't understand what is this platform! Please give me a hint. Thanks",
                                PostTypeId = Domain.Enums.PostType.Question,
                                Views = 53,
                                CreatedAt = DateTime.UtcNow,
                                CreatedByMemberId = communityMember.Id,
                                MemberId = communityMember.Id,
                                CategoryId = category.Id
                            });
                            context.Posts.Add(new Post
                            {
                                Title = "How do I interface with real life?",
                                Body = "I don't understand this concept of hand and motoric movement. I think therefor I move? What is reality actually? Please assist",
                                PostTypeId = Domain.Enums.PostType.Question,
                                Views = 53,
                                CreatedAt = DateTime.UtcNow,
                                CreatedByMemberId = communityMember.Id,
                                MemberId = communityMember.Id,
                                CategoryId = category.Id
                            });
                            context.SaveChanges();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        logger.LogError("Unable to seed the database. Check the connection string in your " +
                            "appsettings file.");
                        throw ex;
                    }
                }
            }
        }
    }
}
