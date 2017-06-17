using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksProcessor.Interfaces
{
    public interface ITimer
    {
        void Start();

        TimeSpan Stop();
    }
}
