using System.Collections.Generic;
using System.IO;

namespace BooksProcessor
{
    public class BooksProcessor
    {
        private IBooksDataProvider booksDataProvider;

        private IBooksParser booksParser;

        private IBooksStorage booksStorage;

        public BooksProcessor(IBooksDataProvider booksDataProvider, IBooksParser booksParser, IBooksStorage booksStorage)
        {
            this.booksDataProvider = booksDataProvider;

            this.booksParser = booksParser;

            this.booksStorage = booksStorage;
        }

        public void ProcessBooks(Stream stream)
        {
            IEnumerable<string> lines = this.booksDataProvider.GetBooksData();
            
            IEnumerable<Book> books = this.booksParser.Parse(lines);
            
            this.booksStorage.Persist(books);
        }
    }
}
