using BrokenEngine.Core;
using System;
using System.IO;

namespace BrokenEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            BrokenHost host = new BrokenHost();

            if (!CreatePluginFolder())
            {
                Console.WriteLine("Error loading Plugins folder");
                Console.ReadKey();
                return;
            }

            host.OnHostEvent += Host_OnHostEvent;

            host.LoadPlugins("Plugins");

            Console.ReadKey();

            host.Close();
        }

        private static void Host_OnHostEvent(object sender, BrokenMessageArgs e)
        {
            if (e.Category == HostEvents.LoadingPluginComplete)
                Console.WriteLine("Plugin loaded: " + e.Message);

            if (e.Category == HostEvents.LoadingAllPluginComplete)
                Console.WriteLine("All plugins loaded");
        }

        static void CreateFolder(string folderPath)
        {
            var dirInfo = new DirectoryInfo(folderPath);
            if (dirInfo.Exists) return;

            dirInfo.Create();
        }

        static bool CreatePluginFolder()
        {
            try
            {
                CreateFolder("Plugins");
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}