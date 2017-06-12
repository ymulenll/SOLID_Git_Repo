using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksProcessor
{
    public class LiteDBBooksStorage : IBooksStorage
    {
        private ILogger logger;

        public LiteDBBooksStorage(ILogger logger)
        {
            this.logger = logger;
        }

        public void Persist(IEnumerable<Book> books)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Books.db");
            using (var db = new LiteDatabase(path))
            {
                var dbBooks = db.GetCollection<Book>("books");

                dbBooks.Insert(books);
            }

            this.logger.LogInfo("{0} books processed", books.Count());
        }
    }
}
