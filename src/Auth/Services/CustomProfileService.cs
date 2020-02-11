using System;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Codidact.Infrastructure.Identity;

namespace Codidact.Auth.Services
{
    public class CustomProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly ILogger<CustomProfileService> _logger;

        public CustomProfileService(UserManager<ApplicationUser> userManager,
           IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
           ILogger<CustomProfileService> logger)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
            _logger = logger;
        }
        /// <summary>
        /// This method is called whenever claims about the user are requested (e.g. during token creation or via the userinfo endpoint)
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject?.GetSubjectId();
            if (sub == null) throw new Exception("No sub claim present");

            var user = await _userManager.FindByIdAsync(sub);
            if (user == null)
            {
                _logger?.LogWarning("No user found matching subject Id: {0}", sub);
            }
            else
            {
                var principal = await _claimsFactory.CreateAsync(user);
                if (principal == null) throw new Exception("ClaimsFactory failed to create a principal");
                if (user.MemberId.HasValue)
                {
                    context.IssuedClaims.Add(
                        new System.Security.Claims.Claim(
                            type: nameof(ApplicationUser.MemberId),
                            value: user.MemberId.Value.ToString())
                        );
                }
                context.AddRequestedClaims(principal.Claims);
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject?.GetSubjectId();
            if (sub == null) throw new Exception("No subject Id claim present");

            var user = await _userManager.FindByIdAsync(sub);
            if (user == null)
            {
                _logger?.LogWarning("No user found matching subject Id: {0}", sub);
            }

            context.IsActive = user != null;
        }
    }
}
