using BooksProcessor.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;

namespace BooksProcessor.UnitTests
{
    [TestFixture]
    class SimpleBooksParserTests
    {
        private IBooksParser booksParser;

        private IBooksValidator booksValidatorFake;

        private IBooksMapper booksMapperFake;

        [SetUp]
        public void Setup()
        {
            this.booksValidatorFake = A.Fake<IBooksValidator>(options => options.Strict());
            this.booksMapperFake = A.Fake<IBooksMapper>(options => options.Strict());

            this.booksParser = new SimpleBooksParser(this.booksValidatorFake, this.booksMapperFake);
        }

        [Test]
        public void BasicParsingWorksFine()
        {
            IEnumerable<string> lines = new [] { "book1|10.1", "book2|20.1" };

            Book[] expected = new[]
            {
                new Book { Title = "book1", Price = 10.1M },
                new Book { Title = "book2", Price = 20.1M }
            };

            A.CallTo(() => this.booksValidatorFake.Validate(A<string[]>._)).Returns(true);
            A.CallTo(() => this.booksMapperFake.Map(A<string[]>._)).ReturnsNextFromSequence(expected);

            var result = this.booksParser.Parse(lines);
            
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
