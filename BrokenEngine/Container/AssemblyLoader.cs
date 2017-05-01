using BrokenEngine.Core;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Runtime.Loader;
using System;

namespace BrokenEngine.Container
{
    /// <summary>
    /// Load plugin and resolve dependencies
    /// </summary>
    class AssemblyLoader
    {
        private string _rootPath;

        public List<FileInfo> LibrariesPath { get; private set; }
        public FileInfo MainLibrary { get; private set; }
        public bool IsValid { get; set; }

        public AssemblyLoader(string folderPath)
        {
            _rootPath = folderPath;
            AssemblyLoadContext.Default.Resolving += ResolveDependencies;
            IsValid = LoadMainFile() && LoadDependencies();
        }

        /// <summary>
        /// Create instance of plugin
        /// </summary>
        /// <returns>Null if interface not implemented</returns>
        public IBrokenPlugin GetPlugin()
        {
            if (!IsValid) return null;

            var pluginAssembly = LoadAssemblyFromFile(MainLibrary);
            foreach(var t in pluginAssembly?.GetExportedTypes() ?? new Type[0])
            {
                if (!t.GetInterfaces().Contains(typeof(IBrokenPlugin)))
                    continue;

                return Activator.CreateInstance(t) as IBrokenPlugin;
            }

            return null;
        }

        /// <summary>
        /// Called when trying to resolve a dependency
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        private Assembly ResolveDependencies(AssemblyLoadContext arg1, AssemblyName arg2)
        {
            var dllFile = LibrariesPath.FirstOrDefault(x => x.Name.IndexOf(arg2.Name) == 0);

            return LoadAssemblyFromFile(dllFile);
        }

        /// <summary>
        /// Load an assembly from a DLL file
        /// </summary>
        /// <param name="info"></param>
        /// <returns>Return null if file not found</returns>
        private Assembly LoadAssemblyFromFile(FileInfo info)
        {
            if (info == null || !info.Exists)
                return null;

            return AssemblyLoadContext.Default.LoadFromAssemblyPath(info.FullName);
        }

        /// <summary>
        /// Search folder for file with pattern BrokenEngine.*.dll
        /// </summary>
        /// <returns>False if not found or if multiple files found</returns>
        private bool LoadMainFile()
        {
            var files = Directory.EnumerateFiles(_rootPath, "BrokenEngine.*.dll", SearchOption.AllDirectories);

            if(files.Count() != 1)
            {
                MainLibrary = null;
                return false;
            }

            MainLibrary = new FileInfo(files.First());
            return true;
        }

        /// <summary>
        /// List all DLL in folder
        /// </summary>
        /// <returns>False if plugin file not yest listed</returns>
        private bool LoadDependencies()
        {
            if (MainLibrary == null) return false;
            LibrariesPath = new List<FileInfo>();

            var files = Directory.EnumerateFiles(_rootPath, "*.dll", SearchOption.AllDirectories);
            foreach(var f in files)
            {
                var info = new FileInfo(f);
                if (info.FullName != MainLibrary.FullName)
                    LibrariesPath.Add(info);
            }

            return true;
        }
    }
}
