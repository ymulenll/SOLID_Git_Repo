using BooksProcessor.Interfaces;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.IO;

namespace BooksProcessor
{
    public class BooksProcessor
    {
        private IBooksDataProvider booksDataProvider;

        private IBooksParser booksParser;

        private IBooksStorage booksStorage;

        public BooksProcessor()
        {
            this.booksDataProvider = ServiceLocator.Current.GetInstance<IBooksDataProvider>();

            this.booksParser = ServiceLocator.Current.GetInstance<IBooksParser>();

            this.booksStorage = ServiceLocator.Current.GetInstance<IBooksStorage>();
        }

        public void ProcessBooks()
        {
            IEnumerable<string> lines = this.booksDataProvider.GetBooksData();
            
            IEnumerable<Book> books = this.booksParser.Parse(lines);
            
            this.booksStorage.Persist(books);
        }
    }
}
