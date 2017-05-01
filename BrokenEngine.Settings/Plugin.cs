using System;
using System.Collections.Generic;
using BrokenEngine.Core;

namespace BrokenEngine.Settings
{
    public class Plugin : IBrokenPlugin
    {
        public ulong LoadPriotity => PluginPriority.CorePriotity;

        public IEnumerable<Type> ServiceDepencies => new List<Type>();

        public string UniqueName => "BrokenPlugin.BasicJsonSettings";

        public string Name => "Settings";

        public int MajorVersion => 1;

        public int MinorVersion => 0;

        public int BuildVersion => 1;

        public bool Initialize(IBrokenHost host)
        {
            if (host.ServiceContainer.HasService<ISettings>())
                return false;

            settings = new Settings();

            //Register ISettings as service
            host.ServiceContainer.RegisterService<ISettings>(settings);

            //Save settings on exit
            host.OnHostEvent += (sender, e) =>
            {
                if (e.Category == HostEvents.Closing) settings?.Save();
            };

            return true;
        }

        public void Uninstall()
        {
            
        }

        private Settings settings;
    }
}
