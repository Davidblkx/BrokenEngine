namespace BrokenEngine.Core
{
    /// <summary>
    /// Map the default values for plugin priority values
    /// </summary>
    public struct PluginPriority
    {
        /// <summary>
        /// Priority: 0,
        /// The absolutely first plugins to load, should be avoid
        /// </summary>
        public const ulong CoreHighPriotity = 0;

        /// <summary>
        /// Priority: 0,
        /// The generally first plugins to load
        /// </summary>
        public const ulong CorePriotity = 5;

        /// <summary>
        /// Priority: 10,
        /// Plugins that at most depend on core priority, 
        /// and only serve as functionality providers for others plugins
        /// </summary>
        public const ulong ProviderPriority = 10;

        /// <summary>
        /// Priority: 100,
        /// Plugins that depend on other plugins, but that also serve as providers
        /// </summary>
        public const ulong AndrogynyPriority = 100;

        /// <summary>
        /// Priority: 1000,
        /// Plugins that heavily use other plugins, and are not expected to provide plugin functionality
        /// </summary>
        public const ulong DefaultPriority = 1000;

        /// <summary>
        /// Priority: 100000
        /// Same as default, but prefer to be loaded at a late time
        /// </summary>
        public const ulong LatePriority = 100000;
    }
}
