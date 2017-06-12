﻿using System;
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
            var booksDataProvider = new StreamBooksDataProvider();
            var logger = new ConsoleLogger();
            var booksValidator = new SimpleBooksValidator(logger);
            var booksMapper = new SimpleBooksMapper();
            var booksParser = new SimpleBooksParser(booksValidator, booksMapper);
            var booksStorage = new LiteDBBooksStorage(logger);
            var booksProcessor = new BooksProcessor(booksDataProvider, booksParser, booksStorage);
            booksProcessor.ProcessBooks();

            Console.ReadKey();
        }
    }
}
