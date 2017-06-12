using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BooksProcessor
{
    public class BooksProcessor
    {
        public void ProcessBooks(Stream stream)
        {
            IEnumerable<string> lines = ReadBooksData(stream);
            
            IEnumerable<Book> books = ParseBooks(lines);
            
            StoreBooks(books);
        }

        private static void StoreBooks(IEnumerable<Book> books)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Books.db");
            using (var db = new LiteDatabase(path))
            {
                var dbBooks = db.GetCollection<Book>("books");

                dbBooks.Insert(books);
            }
            
            LogMessage("INFO: {0} books processed", books.Count());
        }

        private static List<Book> ParseBooks(IEnumerable<string> lines)
        {
            var books = new List<Book>();
            foreach (var line in lines)
            {
                var fields = line.Split('|');
                if (!ValidateBookData(fields))
                {
                    continue;
                }
                
                Book book = MapBookDataToBookRecord(fields);

                books.Add(book);
            }

            return books;
        }

        private static Book MapBookDataToBookRecord(string[] fields)
        {
            var title = fields[0];
            var price = decimal.Parse(fields[1]);

            var book = new Book
            {
                Price = price,
                Title = title
            };

            return book;
        }

        private static bool ValidateBookData(string[] fields)
        {
            if (fields.Length != 2)
            {
                LogMessage("WARN: Line malformed. Only {0} field(s) found.", fields.Length);
                return false;
            }

            if (!decimal.TryParse(fields[1], out decimal price))
            {
                LogMessage("WARN: Book price is not a valid decimal: '{1}'", fields[1]);
                return false;
            }

            return true;
        }

        private static void LogMessage(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        private static IEnumerable<string> ReadBooksData(Stream stream)
        {
            var lines = new List<string>();
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines;
        }
    }
}
