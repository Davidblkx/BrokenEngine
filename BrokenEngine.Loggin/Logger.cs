using System;
using BrokenEngine.Core;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BrokenEngine.Loggin
{
    public class Logger : ILogger
    {
        private readonly LoggerSettings _settings;
        private List<(int, string, DateTime)> _logEntries;
        private StreamWriter _writer;

        const string FILE_NAME = "BrokenEngine.log";

        public Logger(ISettings settings)
        {
            _settings = new LoggerSettings(settings);
            _logEntries = new List<(int, string, DateTime)>();
        }

        public bool WriteToFile
        {
            get { return _settings.WriteToFile; }
            set { _settings.WriteToFile = value; }
        }
        public bool WriteToConsole
        {
            get { return _settings.WriteToConsole; }
            set { _settings.WriteToConsole = value; }
        }
        public bool UseConsoleColors
        {
            get { return _settings.UseConsoleColors; }
            set { _settings.UseConsoleColors = value; }
        }
        public ConsoleColor DebugLevelColor
        {
            get { return _settings.DebugColor; }
            set { _settings.DebugColor = value; }
        }
        public ConsoleColor InfoLevelColor
        {
            get { return _settings.InfoColor; }
            set { _settings.InfoColor = value; }
        }
        public ConsoleColor WarningLevelColor
        {
            get { return _settings.WarningColor; }
            set { _settings.WarningColor = value; }
        }
        public ConsoleColor ErrorLevelColor
        {
            get { return _settings.ErrorColor; }
            set { _settings.ErrorColor = value; }
        }
        public IEnumerable<(int, string, DateTime)> SessionLogs => _logEntries;

        public event Action<object, (int, string)> OnNewEntry;

        public void LogDebugMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void LogErrorMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void LogInfoMessage(string message)
        {
            throw new NotImplementedException();
        }
        
        public void LogWarningMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void LogMessage(int level, string message)
        {
            var entry = (level, message, DateTime.Now);

            _logEntries.Add(entry);
            WriteMessage(entry);
            OnNewEntry?.Invoke(this, (level, message));
        }

        private void WriteMessage((int, string, DateTime) entry)
        {
            var date = entry.Item3.ToString("yyyy-MM-dd|hh:mm:ss");
            var message = $"[{GetLevel(entry.Item1)}]{entry.Item2}[{date}]";

            WriteMessageToConsole(message, entry.Item1);
            WriteMessageToFile(message);
        }

        private string GetLevel(int level)
        {
            if (level < 5)
                return "DEBUG";
            if (level < 10)
                return "INFO";
            if (level < 15)
                return "WARNING";
            if (level < 20)
                return "ERROR";

            return "PSYCHO_ERROR";
        }

        private StreamWriter GetWriter()
        {
            if (_writer == null)
                _writer = new StreamWriter(File.OpenWrite(FILE_NAME));

            return _writer;
        }

        private async Task WriteMessageToFile(string message)
        {
            if (!WriteToFile) return;

            await GetWriter().WriteLineAsync(message);
        }

        private void WriteMessageToConsole(string message, int level)
        {
            if (!WriteToConsole) return;

            if (!UseConsoleColors) Console.WriteLine(message);

            var color = Console.ForegroundColor;

            Console.ForegroundColor = GetConsoleColor(level);
            Console.WriteLine(message);

            Console.ForegroundColor = color;
        }

        private ConsoleColor GetConsoleColor(int level)
        {
            if (level < 5)
                return DebugLevelColor;
            if (level < 10)
                return InfoLevelColor;
            if (level < 15)
                return WarningLevelColor;
            if (level < 20)
                return ErrorLevelColor;

            return ConsoleColor.White;
        }
    }
}
