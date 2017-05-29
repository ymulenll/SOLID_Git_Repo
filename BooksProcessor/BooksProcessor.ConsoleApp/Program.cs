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
            var booksStream = Assembly.GetAssembly(typeof(BooksProcessor)).GetManifestResourceStream("BooksProcessor.NewBooks.txt");

            var booksProcessor = new BooksProcessor();
            booksProcessor.ProcessBooks(booksStream);

            Console.ReadKey();
        }
    }
}
