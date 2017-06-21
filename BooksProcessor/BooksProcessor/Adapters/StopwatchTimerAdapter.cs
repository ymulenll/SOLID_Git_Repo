using BooksProcessor.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksProcessor
{
    public class StopwatchTimerAdapter : ITimer
    {
        private Stopwatch stopwatch;
        public StopwatchTimerAdapter()
        {
            stopwatch = new Stopwatch();
        }

        public void Start()
        {
            this.stopwatch.Start();
        }

        public TimeSpan Stop()
        {
            this.stopwatch.Stop();
            var elapsedMilliseconds = this.stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            return TimeSpan.FromMilliseconds(elapsedMilliseconds);
        }
    }
}
