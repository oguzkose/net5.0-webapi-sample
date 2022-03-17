using System;
using System.Linq;
using FluentAssertions;
using Tests.WebApi.UnitTest.TestSetup;
using WebApi.Applications.BookOperations.Command.DeleteBook;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteBookCommandTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        #region WhenUnavailableBookIdIsGiven_InvalidOperationException_ShouldBeReturn()

        [Theory]
        [InlineData(15)]
        [InlineData(25)]
        [InlineData(4)]
        public void WhenUnavailableBookIdIsGiven_InvalidOperationException_ShouldBeReturn(int bookId)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = bookId;

            FluentActions.Invoking(
                () => command.Handle()
            ).Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be(bookId + " numaralı silinecek kitap bulunamadı");
        }
        #endregion

        #region WhenValidBookIdIsGiven_Book_ShouldBeDeleted()

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenValidBookIdIsGiven_Book_ShouldBeDeleted(int bookId)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = bookId;

            FluentActions.Invoking(
                () => command.Handle()
            ).Invoke();

            var deletedBook = _context.Books.FirstOrDefault(x => x.Id == bookId);

            deletedBook.Should().BeNull();

        }
        #endregion

    }
}