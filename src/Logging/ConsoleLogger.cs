using System;

namespace gsharp.Logging
{
    public class ConsoleLogger : ILogger
    {
        private readonly string? _className;

        public ConsoleLogger(string? className = null)
        {
            _className = className;
        }

        public void Log(string message, string? className = null)
        {
            string prefix = $"[{DateTime.Now:HH:mm:ss}]";
            if (!string.IsNullOrEmpty(_className))
            {
                prefix += $" {_className} :";
            }
            Console.WriteLine($"{prefix} {message}");
        }
    }
}
