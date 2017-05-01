using System;
using System.Collections.Generic;
using BrokenEngine.Core;
using System.Linq;

namespace BrokenEngine.Storage
{
    public class Plugin : IBrokenPlugin
    {
        public ulong LoadPriotity => PluginPriority.ProviderPriority;

        public IEnumerable<Type> ServiceDepencies
            => new Type[]
            {
                typeof(ISettings)
            }.ToList();

        public string UniqueName => "BrokenPlugin.Storage";

        public string Name => "StorageService";

        public int MajorVersion => 1;

        public int MinorVersion => 0;

        public int BuildVersion => 1;

        public bool Initialize(IBrokenHost host)
        {
            var settings = host.ServiceContainer.GetService<ISettings>();
            Storage st = new Storage(settings);

            host.ServiceContainer.RegisterService<IStorage>(st);
            return true;
        }

        public void Uninstall()
        {
            
        }
    }
}
