using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksProcessor.Interfaces;
using Microsoft.Practices.Unity;

namespace BooksProcessor
{
    public class BooksProcessor2 : BooksProcessor
    {
        ILogger logger;
        public BooksProcessor2(IBooksDataProvider booksDataProvider, IBooksParser booksParser, IBooksStorage booksStorage) 
            : base(booksDataProvider, booksParser, booksStorage)
        {
        }

        [InjectionMethod]
        public void Initialize(ILogger logger)
        {
            this.logger = logger;
        }

        public override void ProcessBooks()
        {
            this.logger.LogInfo("begin process");
            base.ProcessBooks();
            this.logger.LogInfo("end process");
        }
    }
}
