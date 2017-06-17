using BooksProcessor.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksProcessor
{
    public class BooksProcessorTimerDecorator : IBooksProcessor
    {
        private IBooksProcessor decoratedBookProcessor;
        private ITimer timer;
        public BooksProcessorTimerDecorator(IBooksProcessor decoratedBookProcessor, ITimer timer)
        {
            this.decoratedBookProcessor = decoratedBookProcessor;
            this.timer = timer;
        }

        public void ProcessBooks()
        {
            this.timer.Start();
            decoratedBookProcessor.ProcessBooks();
            this.timer.Stop();
        }
    }
}
