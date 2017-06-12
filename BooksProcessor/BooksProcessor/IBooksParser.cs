using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksProcessor
{
    public interface IBooksParser
    {
        IEnumerable<Book> Parse(IEnumerable<string> lines);
    }
}
