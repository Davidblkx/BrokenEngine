using BrokenEngine.Core;
using System;
using System.Collections.Generic;

namespace BrokenEngine.Command
{
    public class CommandLine : ICommand
    {
        private Dictionary<string, Func<string[], (bool, string)>> _container;

        public CommandLine()
        {
            _container = new Dictionary<string, Func<string[], (bool, string)>>();
        }

        public (bool, string) Execute(string command, string[] args)
        {
            if (!_container.ContainsKey(command))
                return (false, $"{command} is not a valid command");

            return _container[command](args);
        }

        public bool HasCommand(string command)
        {
            return _container.ContainsKey(command);
        }

        public void Register(string command, Func<string[], (bool, string)> action)
        {
            if (HasCommand(command))
                _container[command] = action;
            else
                _container.Add(command, action);
        }

        public bool Unregister(string command)
        {
            return _container.Remove(command);
        }
    }
}
