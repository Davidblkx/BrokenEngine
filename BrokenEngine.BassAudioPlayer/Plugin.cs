using System;
using System.Collections.Generic;
using BrokenEngine.Core;
using System.Linq;

namespace BrokenEngine.BassAudioPlayer
{
    public class Plugin : IBrokenPlugin
    {
        public ulong LoadPriotity => PluginPriority.AndrogynyPriority;

        public IEnumerable<Type> ServiceDepencies => new Type[]
        {
            typeof(ISettings)
        }.ToList();

        public string UniqueName => "BrokenPlugin.BassAudioPlayer";

        public string Name => "BassAudioPlayer";

        public int MajorVersion => 1;

        public int MinorVersion => 0;

        public int BuildVersion => 1;

        public bool Initialize(IBrokenHost host)
        {
            var settings = host.ServiceContainer.GetService<ISettings>();
            var player = new BassPlayer(settings);
            host.ServiceContainer.RegisterService<IAudioPlayer>(player);

            return true;
        }

        public void Uninstall()
        {

        }
    }
}
