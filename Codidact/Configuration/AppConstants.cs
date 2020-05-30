using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Codidact
{
	public static class AppConstants
	{
		// constants - identity roles
		public const string PolicyAdminOnly = "AdminOnly";
		public const string PolicyModPlus = "ModPlusAdmin";

		public const string RoleAdmin = "Admin";
		public const string RoleModerator = "Moderator";
		public const string RoleTrusted = "TrustedUser";
		public const string RoleBasic = "BasicUser";
		public static string[] RoleNames = { RoleBasic, RoleTrusted, RoleModerator, RoleAdmin };

		// operational constants
		public const int BrowserCacheDuration = 120;	// cache pages on client
		public const int MaxRecordsPerPage = 12;

		public const string KestrelSocket = "/tmp/kestrel-Codidact.sock";
	}

}
