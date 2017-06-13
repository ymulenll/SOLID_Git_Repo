using BooksProcessor.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksProcessor
{
    public class SimpleBooksMapper : IBooksMapper
    {
        public Book Map(string[] fields)
        {
            var title = fields[0];
            var price = decimal.Parse(fields[1], CultureInfo.InvariantCulture);

            var book = new Book
            {
                Price = price,
                Title = title
            };

            return book;
        }
    }
}
