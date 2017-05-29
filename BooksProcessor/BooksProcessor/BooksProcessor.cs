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
            var lines = new List<string>();
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            var books = new List<Book>();
            foreach (var line in lines)
            {
                var fields = line.Split('|');
                if (fields.Length != 2)
                {
                    Console.WriteLine("WARN: Line malformed. Only {0} field(s) found.", fields.Length);
                    continue;
                }

                decimal price;
                if (!decimal.TryParse(fields[1], out price))
                {
                    Console.WriteLine("WARN: Book price is not a valid decimal: '{1}'", fields[1]);
                }

                var title = fields[0];

                var book = new Book
                {
                    Price = price,
                    Title = title
                };

                books.Add(book);
            }

            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Books.db");
            using (var db = new LiteDatabase(path))
            {
                var dbBooks = db.GetCollection<Book>("books");

                dbBooks.Insert(books);
            }
        }
    }
}
