using BooksProcessor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksProcessor
{
    public class LoggingTimerDecorator : ITimer
    {
        private ILogger logger;
        private ITimer decoratedTimmer;
        public LoggingTimerDecorator(ITimer decoratedTimmer, ILogger logger)
        {
            this.logger = logger;
            this.decoratedTimmer = decoratedTimmer;
        }

        public void Start()
        {
            this.decoratedTimmer.Start();
        }

        public TimeSpan Stop()
        {
            var elapsedTime = this.decoratedTimmer.Stop();
            this.logger.LogInfo("The method took {0} secounds to complete", elapsedTime.TotalSeconds);
            return elapsedTime;
        }
    }
}
