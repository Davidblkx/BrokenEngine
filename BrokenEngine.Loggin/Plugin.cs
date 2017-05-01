using BrokenEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrokenEngine.Loggin
{
    public class Plugin : IBrokenPlugin
    {
        public ulong LoadPriotity => PluginPriority.CorePriotity + 1;

        public IEnumerable<Type> ServiceDepencies => 
            new Type[] { typeof(ISettings) }.ToList();

        public string UniqueName => "BrokenEngine.Logging";

        public string Name => "Logging";

        public int MajorVersion => 1;

        public int MinorVersion => 0;

        public int BuildVersion => 1;

        public bool Initialize(IBrokenHost host)
        {
            var logger = new Logger(host.ServiceContainer.GetService<ISettings>());
            host.ServiceContainer.RegisterService<ILogger>(logger);

            return true;
        }

        public void Uninstall()
        {
            throw new NotImplementedException();
        }
    }
}
