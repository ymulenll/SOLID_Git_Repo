using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksProcessor.Interfaces;

namespace BooksProcessor
{
    public class BooksProcessor2 : BooksProcessor
    {
        private IBooksDataProvider booksDataProvider;

        private IBooksParser booksParser;

        private IBooksStorage booksStorage;

        public BooksProcessor2(IBooksDataProvider booksDataProvider, IBooksParser booksParser, IBooksStorage booksStorage)
        {
            this.booksDataProvider = booksDataProvider;

            this.booksParser = booksParser;

            this.booksStorage = booksStorage;
        }

        protected override IEnumerable<string> GetBooksData()
        {
            return this.booksDataProvider.GetBooksData();
        }

        protected override IEnumerable<Book> Parse(IEnumerable<string> lines)
        {
            return this.booksParser.Parse(lines);
        }

        protected override void Persist(IEnumerable<Book> books)
        {
            this.booksStorage.Persist(books);
        }
    }
}
