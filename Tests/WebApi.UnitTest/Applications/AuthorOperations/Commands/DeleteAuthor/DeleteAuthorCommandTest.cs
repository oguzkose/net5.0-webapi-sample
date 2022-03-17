using System;
using System.Linq;
using FluentAssertions;
using Tests.WebApi.UnitTest.TestSetup;
using WebApi.Applications.AuthorOperations.Command.DeleteAuthor;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteAuthorCommandTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }
        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        public void WhenUnavailableAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn(int authorId)
        {
            //Arrange

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = authorId;

            //Act & Assert

            FluentActions.Invoking(
                () => command.Handle()
            ).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek yazar bulunamadı");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void WhenValidAuthoIdIsGiven_Author_ShouldBeDeleted(int authorId)
        {
            //Arrange

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = authorId;

            //Act

            FluentActions.Invoking(
                () => command.Handle()
            ).Invoke();

            //Assert

            var author = _context.Authors.SingleOrDefault(x => x.Id == authorId);
            author.Should().BeNull();

            var booksOfAuthor = _context.Books.Where(x => x.AuthorId == authorId).ToList();
            author.Should().BeNull();

        }
    }
}