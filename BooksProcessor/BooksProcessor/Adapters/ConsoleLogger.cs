using BooksProcessor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksProcessor
{
    public class ConsoleLogger : ILogger
    {
        public void LogError(string message, params object[] args)
        {
            Console.WriteLine("ERROR: " + message, args);
        }

        public void LogInfo(string message, params object[] args)
        {
            Console.WriteLine("INFO: " + message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            Console.WriteLine("WARN: " + message, args);
        }
    }
}
