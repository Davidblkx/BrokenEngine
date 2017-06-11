using System;
using System.Collections.Generic;

namespace BrokenEngine.Core
{
    /// <summary>
    /// Manage known services
    /// </summary>
    public interface IServiceContainer
    {
        /// <summary>
        /// Collection of implementations
        /// </summary>
        IEnumerable<object> ServiceImplementations { get; }
        /// <summary>
        /// Collection of services types
        /// </summary>
        IEnumerable<Type> ServiceTypes { get; }

        /// <summary>
        /// Get service of type <see cref="T"/>
        /// </summary>
        /// <typeparam name="T">Type to search</typeparam>
        /// <returns>default <see cref="T"/> if not found</returns>
        T GetService<T>();
        /// <summary>
        /// Check if service exists
        /// </summary>
        /// <typeparam name="T">Type of service</typeparam>
        /// <returns>True if found, false otherwise</returns>
        bool HasService<T>();
        /// <summary>
        /// Registers a new service in container
        /// </summary>
        /// <typeparam name="T">Type of service to register</typeparam>
        /// <param name="serviceImplementation">service implementation</param>
        /// <returns>True if successfully register, false otherwise</returns>
        bool RegisterService<T>(object serviceImplementation);
        /// <summary>
        /// Remove a service implementation
        /// </summary>
        /// <typeparam name="T">Type of service</typeparam>
        /// <returns>True if removed, false otherwise</returns>
        bool RemoveService<T>();
        /// <summary>
        /// Check if container has all types in collection
        /// </summary>
        /// <param name="serviceList"></param>
        /// <returns></returns>
        bool HasCollection(IEnumerable<Type> serviceList);
    }
}