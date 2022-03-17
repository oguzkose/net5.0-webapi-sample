using System;
using System.Linq;
using FluentAssertions;
using Tests.WebApi.UnitTest.TestSetup;
using WebApi.Applications.BookOperations.Command.UpdateBook;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateBookCommandTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        #region WhenUnavailableBookIdIsGiven_InvalidOperationException_ShouldBeReturn(int bookId)

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void WhenUnavailableBookIdIsGiven_InvalidOperationException_ShouldBeReturn(int bookId)
        {
            //Arrange

            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = bookId;

            //Act & Assert

            FluentActions.Invoking(
                () => command.Handle()
            ).Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Güncellemek istediğiniz kitap bulunamadı");

        }
        #endregion

        #region WhenValidInputsAreGiven_Book_ShouldBeUpdated(int bookId, string title, int authorId, int genreId, int pageCount)

        [Theory]
        [InlineData(1, "Zorba", 2, 2, 540)]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated(int bookId, string title, int authorId, int genreId, int pageCount)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = bookId;
            var newModel = new UpdatedBookModel()
            {
                Title = title,
                AuthorId = authorId,
                GenreId = genreId,
                PageCount = pageCount,
                PublishDate = new DateTime(1992, 04, 21)
            };
            command.Model = newModel;

            //Act

            FluentActions.Invoking(
                () => command.Handle()
            ).Invoke();

            //Assert

            var updatedBook = _context.Books.First(x => x.Id == bookId);

            updatedBook.Title.Should().Be(title);
            updatedBook.AuthorId.Should().Be(authorId);
            updatedBook.GenreId.Should().Be(genreId);
            updatedBook.PageCount.Should().Be(pageCount);
            updatedBook.PublishDate.Should().Be(new DateTime(1992, 04, 21));


        }
        #endregion

    }
}