using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Codidact.Services
{
	/// <summary>
	/// Adds the users ID to the claims cookie.
	/// </summary>
	public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<Users, Role>
	{
		public CustomClaimsPrincipalFactory(UserManager<Users> userManager, RoleManager<Role> roleManager, IOptions<IdentityOptions> optionsAccessor)
			: base(userManager, roleManager, optionsAccessor)
		{
		}

		public async override Task<ClaimsPrincipal> CreateAsync(Users user)
		{
			var principal = await base.CreateAsync(user);

			// Add a claim to the account profile ID for this user.
			((ClaimsIdentity)principal.Identity).AddClaims(new[] { new Claim(ClaimTypes.Sid, $"{user.Id}", ClaimValueTypes.Integer) });

			return principal;
		}
	}

	public static class ClaimsHelper
	{
		public static int GetLoggedOnUserId(ClaimsPrincipal principal)
		{
			try
			{
				if (principal.HasClaim(c => c.Type == System.Security.Claims.ClaimTypes.Sid))
				{
					return Convert.ToInt32(principal.FindFirst(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);
				}
			}
			catch (Exception)
			{
				return 0;
			}

			return 0;
		}
	}
}
