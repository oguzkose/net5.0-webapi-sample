using System;
using System.Linq;
using FluentAssertions;
using Tests.WebApi.UnitTest.TestSetup;
using WebApi.Applications.AuthorOperations.Command.UpdateAuthor;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommandTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        #region WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn(int authorId)

        [Theory]
        [InlineData(7)]
        public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn(int authorId)
        {
            //Arrange

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = authorId;

            //Act & Assert

            FluentActions.Invoking(
                () => command.Handle()
            ).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek yazar bulunamadı");

        }
        #endregion

        [Theory]
        [InlineData(3, "J.R.R.", "Tolkien")]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdated(int authorId, string name, string surname)
        {
            //Arrange

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = authorId;

            UpdateAuthorModel model = new UpdateAuthorModel()
            {
                Name = name,
                Surname = surname,
                DateOfBirth = new DateTime(1892, 01, 01)
            };
            command.Model = model;

            //Act

            FluentActions.Invoking(
                () => command.Handle()
            ).Invoke();

            //Assert

            var newAuthor = _context.Authors.First(x => x.Id == authorId);

            newAuthor.Should().NotBeNull();
            newAuthor.Id.Should().Be(authorId);
            newAuthor.Name.Should().Be(model.Name);
            newAuthor.Surname.Should().Be(model.Surname);
            newAuthor.DateOfBirth.Should().Be(model.DateOfBirth);
        }
    }
}