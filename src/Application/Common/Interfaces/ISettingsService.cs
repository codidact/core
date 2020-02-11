using System;
using System.Collections.Generic;
using System.Text;

namespace Codidact.Application.Common.Interfaces
{
    /// <summary>
    /// Provides the settings for the platform
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Gets the currently used platform from platform settings file
        /// </summary>
        /// <returns>Platform name</returns>
        string GetPlatformName();
    }
}
