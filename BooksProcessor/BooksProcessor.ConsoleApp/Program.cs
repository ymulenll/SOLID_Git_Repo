using BooksProcessor.Interfaces;
using BooksProcessor.LiteDB;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BooksProcessor.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Composition using poor's man dependency injection.
            //PoorManDependencyInjection();

            IoCDependencyInjection();

            Console.ReadKey();
        }

        private static void PoorManDependencyInjection()
        {
            var booksDataProvider = new StreamBooksDataProvider();
            var logger = new ConsoleLogger();
            var booksValidator = new SimpleBooksValidator(logger);
            var booksMapper = new SimpleBooksMapper();
            var booksParser = new SimpleBooksParser(booksValidator, booksMapper);
            var booksStorage = new LiteDBBooksStorage(logger);

            var timer = new StopwatchTimerAdapter();
            var loggerTimer = new LoggingTimerDecorator(timer, logger);
                        
            var booksProcessor = new BooksProcessor(booksDataProvider, booksParser, booksStorage);
            var booksProcessorLogger = new BooksProcessorLoggerDecorator(booksProcessor, logger);
            var booksProcessorLoggerTimer = new BooksProcessorTimerDecorator(booksProcessorLogger, loggerTimer);

            booksProcessorLoggerTimer.ProcessBooks();
        }

        private static void IoCDependencyInjection()
        {
            using (var container = new UnityContainer())
            {
                container.RegisterType<IBooksDataProvider, StreamBooksDataProvider>();
                container.RegisterType<ILogger, ConsoleLogger>();
                container.RegisterType<IBooksValidator, SimpleBooksValidator>();
                container.RegisterType<IBooksMapper, SimpleBooksMapper>();
                container.RegisterType<IBooksParser, SimpleBooksParser>();
                container.RegisterType<IBooksStorage, LiteDBBooksStorage>();

                container.RegisterType<ITimer, LoggingTimerDecorator>(
                    new InjectionConstructor(
                        new ResolvedParameter<StopwatchTimerAdapter>(),
                        new ResolvedParameter<ILogger>()));

                container.RegisterType<IBooksProcessor, BooksProcessor>();
                
                container.RegisterType<IBooksProcessor, BooksProcessorLoggerDecorator>(
                    "BooksProcessorLoggerDecorator",
                    new InjectionConstructor(
                        new ResolvedParameter<IBooksProcessor>(), new ResolvedParameter<ILogger>()));

                container.RegisterType<IBooksProcessor, BooksProcessorTimerDecorator>(
                    "BooksProcessorLoggerTimmingDecorator",
                    new InjectionConstructor(
                        new ResolvedParameter<IBooksProcessor>("BooksProcessorLoggerDecorator"),
                        new ResolvedParameter<ITimer>()));
                
                var booksProcessor = container.Resolve<IBooksProcessor>("BooksProcessorLoggerTimmingDecorator");
                booksProcessor.ProcessBooks();
            }
        }
    }
}
