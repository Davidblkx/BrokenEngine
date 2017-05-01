using System;
using System.Collections.Generic;
using System.Text;

namespace BrokenEngine.Core
{
    /// <summary>
    /// List of Host events
    /// </summary>
    public struct HostEvents
    {
        /// <summary>
        /// When host is about to be closed
        /// </summary>
        public const long Closing = 10;

        /// <summary>
        /// When a plugin is loaded
        /// </summary>
        public const long LoadingPluginComplete = 10004;

        /// <summary>
        /// When all plugins are loaded
        /// </summary>
        public const long LoadingAllPluginComplete = 10005;

        
    }
}
