using BooksProcessor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksProcessor
{
    public class SimpleBooksValidator : IBooksValidator
    {
        private ILogger logger;

        public SimpleBooksValidator(ILogger logger)
        {
            this.logger = logger;
        }

        public bool Validate(string[] fields)
        {
            if (fields.Length != 2)
            {
                this.logger.LogWarning("Line malformed. Only {0} field(s) found.", fields.Length);
                return false;
            }

            if (!decimal.TryParse(fields[1], out decimal price))
            {
                this.logger.LogWarning("Book price is not a valid decimal: '{1}'", fields[1]);
                return false;
            }

            return true;
        }
    }
}
