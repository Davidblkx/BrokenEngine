
using System;
using System.Collections;
using System.Collections.Generic;

namespace BrokenEngine.Core
{
    public interface ILogger
    {
        /// <summary>
        /// Set if log messages are written to disk
        /// </summary>
        bool WriteToFile { get; set; }
        /// <summary>
        /// Set if log messages are written to console
        /// </summary>
        bool WriteToConsole { get; set; }

        /// <summary>
        /// Set if log are written to console using color as distinction 
        /// </summary>
        bool UseConsoleColors { get; set; }
        ConsoleColor DebugLevelColor { get; set; }
        ConsoleColor InfoLevelColor { get; set; }
        ConsoleColor WarningLevelColor { get; set; }
        ConsoleColor ErrorLevelColor { get; set; }

        /// <summary>
        /// Log a message to system
        /// </summary>
        /// <param name="level">threat level, 0 - debug</param>
        /// <param name="message">log message</param>
        void LogMessage(int level, string message);

        /// <summary>
        /// Log message with threat DEBUG
        /// </summary>
        /// <param name="message">Log message</param>
        void LogDebugMessage(string message);
        /// <summary>
        /// Log message with threat INFO
        /// </summary>
        /// <param name="message">Log message</param>
        void LogInfoMessage(string message);
        /// <summary>
        /// Log message with threat WARNING
        /// </summary>
        /// <param name="message">Log message</param>
        void LogWarningMessage(string message);
        /// <summary>
        /// Log message with threat ERROR
        /// </summary>
        /// <param name="message">Log message</param>
        void LogErrorMessage(string message);

        IEnumerable<(int, string, DateTime)> SessionLogs { get; }

        event Action<object, (int, string)> OnNewEntry;
    }
}
