using System.Collections.Generic;

namespace BrokenEngine.Core
{
    /// <summary>
    /// Interface shared between plugins
    /// </summary>
    public interface IBrokenHost
    {
        /// <summary>
        /// Entry point to handle services
        /// </summary>
        IServiceContainer ServiceContainer { get; }

        /// <summary>
        /// Raise an OnPluginEvent message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="messageArguments"></param>
        void BroadcastMessage(object sender, BrokenMessageArgs messageArguments);

        /// <summary>
        /// Events raised by the Host application
        /// </summary>
        event BrokenMessageEvent OnHostEvent;
        /// <summary>
        /// Events raised by a plugin
        /// </summary>
        event BrokenMessageEvent OnPluginEvent;

        /// <summary>
        /// Attempt to load plugins from specified directory
        /// </summary>
        /// <param name="directoryPath"></param>
        void LoadPlugins(string directoryPath);
    }
}
