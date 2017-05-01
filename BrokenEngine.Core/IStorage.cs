using System;
using System.IO;

namespace BrokenEngine.Core
{
    public interface IStorage
    {
        /// <summary>
        /// Defines a folder where to look for plugins
        /// </summary>
        DirectoryInfo PluginsFolder { get; set; }
        /// <summary>
        /// Folder used by plugins to storage persistent data
        /// </summary>
        DirectoryInfo DataFolder { get; set; }
        /// <summary>
        /// Folder used by plugins to storage temp data
        /// </summary>
        DirectoryInfo TempFolder { get; set; }

        /// <summary>
        /// Get directory info for relative path, create new folders if needed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DirectoryInfo GetStorage(string path);

        /// <summary>
        /// Raised every time data folder path is changed
        /// </summary>
        event Action OnDataFolderChanged;
        /// <summary>
        /// Raised every time plugins folder is changed
        /// </summary>
        event Action OnPluginsFolderChanged;
    }
}
