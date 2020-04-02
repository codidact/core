using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Codidact
{
	public class AppSettings
	{
		// directory name for uploaded images
		public string ImageRoot { get; set; }
		// directory name for thumbnails
		public string ThumbRoot { get; set; }
		// directory name for attached files
		public string AttachmentRoot { get; set; }

		public string EmailUser { get; set; }
		public string EmailFrom { get; set; }
		public string EmailServer { get; set; }
		public int EmailPort { get; set; }
		public string EmailPassword { get; set; }

		public string KeyStorageDirectory { get; set; }

		public List<string> InvalidUsernames { get; set; }


		// ---------------------------------------------------
		// calculated values - full local paths to directories
		public string ImageRootPath { get; set; }
		public string ThumbRootPath { get; set; }
		public string AttachRootPath { get; set; }

		// calculated from theme css files on disk
		public SelectList Themes { get; set; }

		// fixed settings
		public int AdminUID { get; set; }
		public string AdminName { get; set; }

		public string DefaultCountry { get; set; } = "UK";
	}


	public class UsersConfig
	{
		public string AdminName { get; set; }
		public string AdminEmail { get; set; }
		public string AdminPassword { get; set; }
	}


}
