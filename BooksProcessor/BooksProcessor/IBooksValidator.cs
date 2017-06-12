using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksProcessor
{
    public interface IBooksValidator
    {
        bool Validate(string[] fields);
    }
}
