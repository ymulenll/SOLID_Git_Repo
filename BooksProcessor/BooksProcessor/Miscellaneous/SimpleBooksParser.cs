using BooksProcessor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksProcessor
{
    public class SimpleBooksParser : IBooksParser
    {
        private IBooksValidator booksValidator;

        private IBooksMapper booksMapper;

        public SimpleBooksParser(IBooksValidator booksValidator, IBooksMapper booksMapper)
        {
            this.booksValidator = booksValidator;
            this.booksMapper = booksMapper;
        }

        public IEnumerable<Book> Parse(IEnumerable<string> lines)
        {
            var books = new List<Book>();
            foreach (var line in lines)
            {
                var fields = line.Split('|');
                if (!this.booksValidator.Validate(fields))
                {
                    continue;
                }

                Book book = this.booksMapper.Map(fields);

                books.Add(book);
            }

            return books;
        }
    }
}
