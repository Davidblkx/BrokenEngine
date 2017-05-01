using System;
using System.Collections.Generic;

namespace BrokenEngine.Core
{
    /// <summary>
    /// Basic plugin interface
    /// </summary>
    public interface IBrokenPlugin
    {
        /// <summary>
        /// Plugin load priority, where 0 is highest priority
        /// See, [PluginPriority] for a guideline
        /// </summary>
        ulong LoadPriotity { get; }

        /// <summary>
        /// Collection of services types that this service depends on,
        /// this collection is used at load time to handle load order
        /// </summary>
        IEnumerable<Type> ServiceDepencies { get;  }

        /// <summary>
        /// An unique name to identify this plugin, should be constant across versions
        /// Its used in storage and settings handle, should be path safe
        /// </summary>
        string UniqueName { get; }

        /// <summary>
        /// Friendly name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// In version 1.3.45 it's the 1,
        /// Should change when changes broke functionality for previous versions 
        /// </summary>
        int MajorVersion { get; }
        /// <summary>
        /// In version 1.3.45 its the 3,
        /// Should change when new functionality is added
        /// </summary>
        int MinorVersion { get; }
        /// <summary>
        /// In version 1.3.45 its the 45,
        /// Should change when bugs are fixed
        /// </summary>
        int BuildVersion { get; }

        /// <summary>
        /// Called at load time
        /// </summary>
        /// <param name="host">The app host</param>
        /// <returns>False to delay the load, or when a error occurs</returns>
        bool Initialize(IBrokenHost host);

        /// <summary>
        /// Called when the user uninstalls 
        /// </summary>
        void Uninstall();
    }
}