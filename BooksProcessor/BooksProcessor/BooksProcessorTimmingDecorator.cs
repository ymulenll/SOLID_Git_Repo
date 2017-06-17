using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksProcessor
{
    public class BooksProcessorTimmingDecorator : IBooksProcessor
    {
        private IBooksProcessor decoratedBookProcessor;
        private Stopwatch stopwatch;
        public BooksProcessorTimmingDecorator(IBooksProcessor decoratedBookProcessor)
        {
            this.decoratedBookProcessor = decoratedBookProcessor;
            stopwatch = new Stopwatch();
        }

        public void ProcessBooks()
        {
            stopwatch.Start();
            decoratedBookProcessor.ProcessBooks();
            stopwatch.Stop();
            Console.WriteLine("The method took {0} secounds to complete", TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).TotalSeconds);
        }
    }
}
