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
    public class SimpleBooksValidatorTests
    {
        private IBooksValidator booksValidator;
        private ILogger loggerFake;

        [SetUp]
        public void Setup()
        {
            this.loggerFake = A.Fake<ILogger>(options => options.Strict());
            this.booksValidator = new SimpleBooksValidator(this.loggerFake);
        }

        [Test]
        public void BasicValidationReturnsTrue()
        {
            string[] fields = { "hello", "25.36" };
            var result = this.booksValidator.Validate(fields);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void WrongPriceLogsWarning()
        {
            A.CallTo(() => this.loggerFake.LogWarning(A<string>._, A<object>._)).DoesNothing();
            string[] fields = { "hello", "wrong price" };
            this.booksValidator.Validate(fields); 
            A.CallTo(() => this.loggerFake.LogWarning(A<string>._, A<object>._)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void WrongPriceRetursFalse()
        {
            A.CallTo(() => this.loggerFake.LogWarning(A<string>._, A<object>._)).DoesNothing();
            string[] fields = { "hello", "wrong price" };
            var result = this.booksValidator.Validate(fields);
            Assert.AreEqual(false, result);
        }
    }
}
