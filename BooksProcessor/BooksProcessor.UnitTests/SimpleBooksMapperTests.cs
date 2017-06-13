using BooksProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksProcessor.Interfaces;
using NUnit.Framework;

namespace BooksProcessor.UnitTests
{
    [TestFixture]
    public class SimpleBooksMapperTests
    {
        IBooksMapper booksMapper = new SimpleBooksMapper();

        [Test]
        public void BasicMapWorksFine()
        {
            string[] fields = { "hello", "25.36" };
            var result = booksMapper.Map(fields);
            Assert.AreEqual("hello", result.Title);
            Assert.AreEqual(25.36, result.Price);
        }
    }
}