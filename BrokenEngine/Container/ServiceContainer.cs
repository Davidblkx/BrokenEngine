using BrokenEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BrokenEngine.Container
{
    internal class ServiceContainer : IServiceContainer
    {
        private readonly Dictionary<Type, object> _container;

        public ServiceContainer()
        {
            _container = new Dictionary<Type, object>();
        }

        public bool HasService<T>()
        {
            var type = typeof(T);

            return _container.ContainsKey(type);
        }

        public T GetService<T>()
        {
            var type = typeof(T);

            if (!_container.ContainsKey(type)) return default(T);

            var item = _container[type];

            if (item is T) return (T)item;

            return default(T);
        }

        public bool RegisterService<T>(object serviceImplementation)
        {
            if (HasService<T>()) return false;

            if (!(serviceImplementation is T)) return false;

            _container.Add(typeof(T), serviceImplementation);

            return true;
        }

        public bool RemoveService<T>()
        {
            return _container.Remove(typeof(T));
        }

        public bool HasCollection(IEnumerable<Type> serviceList)
        {
            return ServiceTypes.Where(x => serviceList.Contains(x)).Count() == serviceList.Count();
        }

        public IEnumerable<Type> ServiceTypes => _container.Keys;
        public IEnumerable<object> ServiceImplementations => _container.Values;
    }
}
