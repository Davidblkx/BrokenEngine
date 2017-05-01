using System;
using System.Collections.Generic;
using System.Text;

namespace BrokenEngine.Core
{
    public interface ICommand
    {
        /// <summary>
        /// Sends an execute request to command
        /// </summary>
        /// <param name="command"></param>
        /// <param name="args"></param>
        /// <returns>(end status, message)</returns>
        (bool, string) Execute(string command, string[] args);

        /// <summary>
        /// Checks if command name is register
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        bool HasCommand(string command);

        /// <summary>
        /// Register command as action
        /// </summary>
        /// <param name="command"></param>
        /// <param name="action"></param>
        void Register(string command, Func<string[], (bool, string)> action);

        /// <summary>
        /// Remove command action
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        bool Unregister(string command);
    }
}
