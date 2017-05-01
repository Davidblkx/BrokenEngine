using BrokenEngine.Core;
using System;

namespace BrokenEngine.Loggin
{
    public class LoggerSettings
    {
        private readonly ISettings _settings;

        private const string KEY_WRITE_FILE = "logger.writeToFile";
        private const string KEY_WRITE_CONSOLE = "logger.writeToConsole";
        private const string KEY_USE_COLORS = "logger.useColors";

        private const string KEY_COLOR_DEBUG = "logger.color.debug";
        private const string KEY_COLOR_INFO = "logger.color.info";
        private const string KEY_COLOR_WARNING = "logger.color.warning";
        private const string KEY_COLOR_ERROR = "logger.color.error";

        public LoggerSettings(ISettings settings)
        {
            _settings = settings;
            _settings.RegisterConverter(ParseColorFromString);
        }

        ConsoleColor ParseColorFromString(string colorValue)
        {
            if (string.IsNullOrEmpty(colorValue)) return ConsoleColor.White;

            return (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorValue);
        }
        ConsoleColor GetColorFromSettings(string key, ConsoleColor defValue)
        {
            var (success, value) = _settings.GetValue(key, defValue);

            if (!success)
            {
                _settings.SetValue(key, defValue);
                return defValue;
            }

            return value;
        }

        public bool WriteToFile
        {
            get { return _settings.GetValue(KEY_WRITE_FILE, false).Item2; }
            set { _settings.SetValue(KEY_WRITE_FILE, value); }
        }

        public bool WriteToConsole
        {
            get { return _settings.GetValue(KEY_WRITE_CONSOLE, false).Item2; }
            set { _settings.SetValue(KEY_WRITE_CONSOLE, value); }
        }

        public bool UseConsoleColors
        {
            get { return _settings.GetValue(KEY_USE_COLORS, false).Item2; }
            set { _settings.SetValue(KEY_USE_COLORS, value); }
        }

        public ConsoleColor DebugColor
        {
            get { return GetColorFromSettings(KEY_COLOR_DEBUG, ConsoleColor.White); }
            set { _settings.SetValue(KEY_COLOR_DEBUG, value); }
        }

        public ConsoleColor InfoColor
        {
            get { return GetColorFromSettings(KEY_COLOR_DEBUG, ConsoleColor.DarkGreen); }
            set { _settings.SetValue(KEY_COLOR_DEBUG, value); }
        }

        public ConsoleColor WarningColor
        {
            get { return GetColorFromSettings(KEY_COLOR_DEBUG, ConsoleColor.Yellow); }
            set { _settings.SetValue(KEY_COLOR_DEBUG, value); }
        }

        public ConsoleColor ErrorColor
        {
            get { return GetColorFromSettings(KEY_COLOR_DEBUG, ConsoleColor.Red); }
            set { _settings.SetValue(KEY_COLOR_DEBUG, value); }
        }
    }
}
