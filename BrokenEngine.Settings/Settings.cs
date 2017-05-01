using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using BrokenEngine.Core;
using System;

namespace BrokenEngine.Settings
{
    public class Settings : ISettings
    {
        public Settings()
        {
            if (!Load())
            {
                _values = new Dictionary<string, string>();
                Save();
            }

            _converters = new ConverterManager();
        }

        private Dictionary<string, string> _values;
        private ConverterManager _converters;

        private const string FILE_NAME = "broken.settings";

        public event Action<string, string> OnKeyValueChange;

        public string GetValue(string key)
        {
            if (_values.ContainsKey(key))
                return _values[key];

            return null;
        }

        public string GetValue(string key, string defaultValue)
        {
            if (!_values.ContainsKey(key))
                SetValue(key, defaultValue);

            return _values[key];
        }

        public void SetValue(string key, string value)
        {
            if (_values.ContainsKey(key) && _values[key] != value)
            {
                _values[key] = value;
                RaiseEvent(key, value);
            }
            else
            {
                _values.Add(key, value);
                RaiseEvent(key, value);
            }
        }

        public void SetValue<T>(string key, T value)
        {
            SetValue(key, value.ToString());
        }

        public void Save()
        {
            var jsonString = JsonConvert.SerializeObject(_values, Formatting.Indented);
            File.WriteAllText(FILE_NAME, jsonString);
        }

        /// <summary>
        /// Try to parse file into _values
        /// </summary>
        /// <returns></returns>
        protected bool Load()
        {
            if(!File.Exists(FILE_NAME))
                return false;

            try
            {
                var jsonString = File.ReadAllText(FILE_NAME);
                _values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public (bool, T) GetValue<T>(string key)
        {
            if (!_converters.HasConverter<T>())
                return (false, default(T));

            return (true, _converters.Convert<T>(GetValue(key)));
        }

        public (bool, T) GetValue<T>(string key, T defaultValue)
        {
            if (!_converters.HasConverter<T>())
                return (false, default(T));

            return (true, _converters.Convert<T>(GetValue(key, defaultValue.ToString())));
        }

        public T GetValue<T>(string key, Func<string, T> converter)
        {
            return converter(GetValue(key));
        }

        public T GetValue<T>(string key, string defaultValue, Func<string, T> converter)
        {
            return converter(GetValue(key, defaultValue));
        }

        public void RegisterConverter<T>(Func<string, T> converter)
        {
            _converters.SetConverter<T>(converter);
        }

        private void RaiseEvent(string key, string value)
        {
            OnKeyValueChange?.Invoke(key, value);
        }

        public bool HasConverter<T>()
        {
            return _converters.HasConverter<T>();
        }
    }
}
