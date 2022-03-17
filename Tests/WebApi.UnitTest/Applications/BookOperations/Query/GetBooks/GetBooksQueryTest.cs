using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTest.TestSetup;
using WebApi.Applications.BookOperations.Query.GetBooks;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.BookOperations.Query.GetBooks
{
    public class GetBooksQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBooksQueryTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        #region WhenBooksIsNull_InvalidOperationException_ShouldBeReturn()
        [Fact]
        public void WhenBooksIsNull_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var currentBooks = _context.Books.OrderBy(x => x.Id);
            _context.RemoveRange(currentBooks);
            _context.SaveChanges();

            GetBooksQuery query = new GetBooksQuery(_context, null);

            //Act & Assert
            FluentActions.Invoking(
                () => query.Handle()
            ).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı");
        }
        #endregion

        #region WhenBooksIsNotNull_Books_ShouldBeReturn()

        [Fact]
        public void WhenBooksIsNotNull_Books_ShouldBeReturn()
        {
            //Arrange

            GetBooksQuery query = new GetBooksQuery(_context, _mapper);

            //Act

            var result = FluentActions.Invoking(
                () => query.Handle()
            ).Invoke();

            //Assert

            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Count.Should().BeGreaterThan(0);
        }
        #endregion

    }
}
