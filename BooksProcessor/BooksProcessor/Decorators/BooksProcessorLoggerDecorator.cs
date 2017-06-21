using BooksProcessor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksProcessor
{
    public class BooksProcessorLoggerDecorator : IBooksProcessor
    {
        IBooksProcessor decoratedBooksProcessor;
        ILogger logger;
        public BooksProcessorLoggerDecorator(IBooksProcessor decoratedBooksProcessor, ILogger logger)
        {
            this.decoratedBooksProcessor = decoratedBooksProcessor;
            this.logger = logger;
        }

        public void ProcessBooks()
        {
            this.logger.LogInfo("Begin method.");
            this.decoratedBooksProcessor.ProcessBooks();
            this.logger.LogInfo("End method.");
        }
    }
}
