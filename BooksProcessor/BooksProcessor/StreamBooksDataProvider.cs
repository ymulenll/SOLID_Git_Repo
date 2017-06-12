using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BooksProcessor
{
    class StreamBooksDataProvider : IBooksDataProvider
    {
        public IEnumerable<string> GetBooksData()
        {
            var booksStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BooksProcessor.NewBooks.txt");
            var lines = new List<string>();
            using (var reader = new StreamReader(booksStream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines;
        }
    }
}
