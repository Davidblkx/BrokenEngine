using BrokenEngine.Core;
using System.IO;
using System;

namespace BrokenEngine.Storage
{
    public class Storage : IStorage
    {
        private ISettings _settings;

        const string KEY_PLUGINS = "default.storage.plugins";
        const string KEY_DATA = "default.storage.data";
        const string KEY_TEMP = "default.storage.temp";

        public Storage(ISettings settings)
        {
            _settings = settings;

            PluginsFolder.Create();
            DataFolder.Create();
            TempFolder.Create();
        }

        public DirectoryInfo PluginsFolder
        {
            get { return new DirectoryInfo(_settings.GetValue(KEY_PLUGINS, "Plugins")); }
            set
            {
                var previous = _settings.GetValue(KEY_TEMP);
                if (!value.Exists || previous == value.FullName) return;

                _settings.SetValue(KEY_TEMP, value.FullName);
                OnDataFolderChanged?.Invoke();
            }
        }
        public DirectoryInfo DataFolder
        {
            get { return new DirectoryInfo(_settings.GetValue(KEY_DATA, "Data")); }
            set
            {
                var previous = _settings.GetValue(KEY_DATA);
                if (!value.Exists || previous == value.FullName) return;

                _settings.SetValue(KEY_DATA, value.FullName);
                OnDataFolderChanged?.Invoke();
            }
        }
        public DirectoryInfo TempFolder
        {
            get { return new DirectoryInfo(_settings.GetValue(KEY_TEMP, "Temp")); }
            set
            {
                if (value.Exists)
                    _settings.SetValue(KEY_TEMP, value.FullName);
            }
        }

        public event Action OnDataFolderChanged;
        public event Action OnPluginsFolderChanged;

        public DirectoryInfo GetStorage(string path)
        {
            var data = DataFolder.FullName;
            var fullPath = Path.Combine(data, path);

            var dir = new DirectoryInfo(fullPath);
            if (!dir.Exists)
                dir.Create();

            return dir;
        }
    }
}
