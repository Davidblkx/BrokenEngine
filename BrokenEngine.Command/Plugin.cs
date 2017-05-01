using System;
using System.Collections.Generic;
using BrokenEngine.Core;
using System.Linq;

namespace BrokenEngine.Command
{
    public class Plugin : IBrokenPlugin
    {
        public ulong LoadPriotity => PluginPriority.CorePriotity;

        public IEnumerable<Type> ServiceDepencies => new List<Type>();

        public string UniqueName => "BrokenPlugin.BasicCommandLine";

        public string Name => "CommandLine";

        public int MajorVersion => 1;

        public int MinorVersion => 0;

        public int BuildVersion => 1;

        public bool Initialize(IBrokenHost host)
        {
            cmd = new CommandLine();
            host.ServiceContainer.RegisterService<ICommand>(cmd);

            return true;
        }

        public void Uninstall()
        {

        }

        private CommandLine cmd;
    }
}
