using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksProcessor
{
    public interface ILogger
    {
        void LogWarning(string message, params object[] args);

        void LogInfo(string message, params object[] args);

        void LogError(string message, params object[] args);
    }
}
