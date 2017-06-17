using BooksProcessor.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace BooksProcessor
{
    public abstract class BooksProcessor
    {
        protected abstract IEnumerable<string> GetBooksData();
        protected abstract IEnumerable<Book> Parse(IEnumerable<string> lines);
        protected abstract void Persist(IEnumerable<Book> books);

        public void ProcessBooks()
        {
            // code
            IEnumerable<string> lines = this.GetBooksData();

            // code
            IEnumerable<Book> books = this.Parse(lines);
            
            // code
            this.Persist(books);

            // code
        }
    }
}
