using System;
using System.Collections.Generic;
using System.Text;

namespace BrokenEngine.Settings
{
    public class ConverterManager
    {
        private Dictionary<Type, object> _converters;

        public ConverterManager()
        {
            _converters = new Dictionary<Type, object>();
            LoadDefault();
        }

        public bool HasConverter<T>()
        {
            return _converters.ContainsKey(typeof(T));
        }

        public Func<string, T> GetConverter<T>()
        {
            if (!HasConverter<T>())
                throw new NotImplementedException($"Converter not implemented for {typeof(T).Name}");

            return (Func<string, T>)_converters[typeof(T)];
        }

        public void SetConverter<T>(Func<string, T> converter)
        {
            if (HasConverter<T>())
                _converters[typeof(T)] = converter;
            else
                _converters.Add(typeof(T), converter);
        }

        public T Convert<T>(string value)
        {
            var converter = GetConverter<T>();
            return converter(value);
        }

        private void LoadDefault()
        {
            SetConverter<int>(x =>
            {
                if (x == null) return -1;

                return int.Parse(x);
            });

            SetConverter<double>(x =>
            {
                if (x == null) return -1;

                return double.Parse(x);
            });

            SetConverter<float>(x =>
            {
                if (x == null) return -1;

                return float.Parse(x);
            });

            SetConverter<decimal>(x =>
            {
                if (x == null) return -1;

                return decimal.Parse(x);
            });

            SetConverter<bool>(x =>
            {
                if (x == null) return false;

                return bool.Parse(x);
            });

            SetConverter<DateTime>(x =>
            {
                if (x == null) return DateTime.Now;

                return DateTime.Parse(x);
            });
        }
    }
}
