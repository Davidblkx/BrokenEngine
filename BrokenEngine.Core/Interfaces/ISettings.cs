using System;

namespace BrokenEngine.Core
{
    /// <summary>
    /// Save and load settings
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        /// Get the value for specified key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>null, if not found</returns>
        string GetValue(string key);

        /// <summary>
        /// Get or initialize the value for specified key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">Value to initialize</param>
        /// <returns></returns>
        string GetValue(string key, string defaultValue);

        /// <summary>
        /// Defines the value for specified key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetValue(string key, string value);

        void SetValue<T>(string key, T value);

        /// <summary>
        /// Get value for specified key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>Value indicating conversion success, conversion result</returns>
        (bool, T) GetValue<T>(string key);

        /// <summary>
        /// Get or initialize the value for specified key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>Value indicating conversion success, conversion result</returns>
        (bool, T) GetValue<T>(string key, T defaultValue);

        /// <summary>
        /// Get value, converting it to T using FUNC<>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        T GetValue<T>(string key, Func<string, T> converter);

        /// <summary>
        /// Get or initialize value, converting it to T using FUNC<>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        T GetValue<T>(string key, string defaultValue, Func<string, T> converter);

        /// <summary>
        /// Add a new converter to source
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="converter"></param>
        void RegisterConverter<T>(Func<string, T> converter);

        /// <summary>
        /// Checks if converter for type T is defined
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool HasConverter<T>();

        /// <summary>
        /// Flush settings to file
        /// </summary>
        void Save();

        /// <summary>
        /// Raised every time a KeyValuePair is changed
        /// </summary>
        event Action<string,string> OnKeyValueChange;
    }
}
