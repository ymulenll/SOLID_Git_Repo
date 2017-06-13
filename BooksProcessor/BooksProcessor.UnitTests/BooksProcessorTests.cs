using BooksProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksProcessor.Interfaces;
using NUnit.Framework;
using FakeItEasy;

namespace BooksProcessor.UnitTests
{
    [TestFixture]
    class BooksProcessorTests
    {
        private BooksProcessor booksProcessor;

        private IBooksDataProvider booksDataProviderFake;

        private IBooksParser booksParserFake;

        private IBooksStorage booksStorageFake;

        [SetUp]
        public void Setup()
        {
            this.booksDataProviderFake = A.Fake<IBooksDataProvider>(options => options.Strict());

            this.booksParserFake = A.Fake<IBooksParser>(options => options.Strict());

            this.booksStorageFake = A.Fake<IBooksStorage>(options => options.Strict());

            this.booksProcessor = new BooksProcessor(this.booksDataProviderFake, this.booksParserFake, this.booksStorageFake);
        }

        [Test]
        public void BasicProcessingOrchestratesFine()
        {
            var lines = new[] { "book1|1.5" };
            var books = new[] { new Book { Title = "book1", Price = 1.5M } };
            A.CallTo(() => this.booksDataProviderFake.GetBooksData()).Returns(lines);
            A.CallTo(() => this.booksParserFake.Parse(lines)).Returns(books);
            A.CallTo(() => this.booksStorageFake.Persist(books)).DoesNothing();

            this.booksProcessor.ProcessBooks();

            A.CallTo(() => this.booksDataProviderFake.GetBooksData()).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => this.booksParserFake.Parse(lines)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => this.booksStorageFake.Persist(books)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
