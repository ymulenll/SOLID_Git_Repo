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
            var booksProcessor = new BooksProcessor(booksDataProvider, booksParser, booksStorage);
            booksProcessor.ProcessBooks();
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
                container.RegisterType<BooksProcessor>();

                var booksProcessor = container.Resolve<BooksProcessor>();
                booksProcessor.ProcessBooks();
            }
        }
    }
}
