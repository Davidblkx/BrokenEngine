using BrokenEngine.Core;
using BrokenEngine.Container;
using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BrokenEngine
{
    public class BrokenHost : IBrokenHost
    {
        public BrokenHost()
        {
            ServiceContainer = new ServiceContainer();
        }

        public IServiceContainer ServiceContainer { get; private set; }

        public event BrokenMessageEvent OnHostEvent;
        public event BrokenMessageEvent OnPluginEvent;

        public void BroadcastMessage(object sender, BrokenMessageArgs messageArguments)
        {
            OnPluginEvent?.Invoke(sender, messageArguments);
        }
        public void BroadcastAsHost(BrokenMessageArgs messageArguments)
        {
            OnHostEvent?.Invoke(this, messageArguments);
        }

        public void LoadPlugins(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                return;

            var pluginCollection = GetPluginsFromFolder(directoryPath);

            InitilizePluginCollection(pluginCollection);

            //Broadcast all plugin loaded message
            BroadcastAsHost(new BrokenMessageArgs
            {
                Category = HostEvents.LoadingAllPluginComplete,
                Message = ""
            });
        }

        /// <summary>
        /// For each top directory in folder attempt to load a plugin
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        private IEnumerable<IBrokenPlugin> GetPluginsFromFolder(string directoryPath)
        {
            var directories = Directory.EnumerateDirectories(directoryPath);
            foreach (var dir in directories)
            {
                AssemblyLoader loader = new AssemblyLoader(dir);
                var plugin = loader.GetPlugin();

                if (plugin != null)
                    yield return plugin;
            }
        }

        /// <summary>
        /// Raise new plugin message
        /// </summary>
        /// <param name="pluginName"></param>
        private void BroadcastNewPlugin(string pluginName)
        {
            BroadcastAsHost(new BrokenMessageArgs
            {
                Category = HostEvents.LoadingPluginComplete,
                Message = pluginName
            });
        }

        /// <summary>
        /// Attempt to load a plugin collection
        /// </summary>
        /// <param name="plugins"></param>
        private void InitilizePluginCollection(IEnumerable<IBrokenPlugin> plugins)
        {
            var orderPlugins = new Queue<IBrokenPlugin>(plugins.OrderBy(x => x.LoadPriotity));
            var pluginCount = orderPlugins.Count;
            var skippedPlugins = new List<IBrokenPlugin>();

            while(orderPlugins.Count > 0)
            {
                var plugin = orderPlugins.Dequeue();

                //If plugin dependencies are not loaded, skip plugin
                if(!ValidatePluginDependencies(plugin.ServiceDepencies))
                {
                    skippedPlugins.Add(plugin);
                    continue;
                }

                //If initialization failed log error to console
                if (!plugin.Initialize(this))
                {
                    Console.WriteLine("Error loading plugin: ", plugin.UniqueName);
                    continue;
                }

                BroadcastNewPlugin(plugin.UniqueName);
            }

            //Try to load skipped plugins, only if new plugins were loaded
            if (skippedPlugins.Count > 0 && skippedPlugins.Count < pluginCount)
                InitilizePluginCollection(skippedPlugins);
            else if (skippedPlugins.Count > 0)
                foreach (var p in skippedPlugins)
                    Console.WriteLine("Dependencies not found for: ", p.UniqueName);
        }

        /// <summary>
        /// Check if all dependencies exist in service container
        /// </summary>
        /// <param name="dependencies"></param>
        /// <returns></returns>
        private bool ValidatePluginDependencies(IEnumerable<Type> dependencies)
        {
            if (dependencies.Count() == 0) return true;
            return ServiceContainer.HasCollection(dependencies);
        }

        public void Close()
        {
            BroadcastAsHost(new BrokenMessageArgs
            {
                Category = HostEvents.Closing,
            });
        }
    }
}
