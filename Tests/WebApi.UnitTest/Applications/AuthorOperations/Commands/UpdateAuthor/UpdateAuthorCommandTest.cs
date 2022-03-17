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

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        public void WhenUnavailableAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn(int authorId)
        {
            //Arrange

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = authorId;

            //Act & Assert
            FluentActions.Invoking(
                () => command.Handle()
            ).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek yazar bulunamadı");


        }



        [Theory]
        [InlineData(1, "Nikos", "Kazancakis")]
        public void TestName(int authorId, string name, string surname)
        {
            //Arrange

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = authorId;

            command.Model = new UpdateAuthorModel()
            {
                Name = name,
                Surname = surname,
                DateOfBirth = new DateTime(1953, 12, 25)
            };

            //Act

            FluentActions.Invoking(
                () => command.Handle()
            ).Invoke();


            //Assert

            var updatedAuthor = _context.Authors.First(x => x.Id == authorId);

            updatedAuthor.Should().NotBeNull();
            updatedAuthor.Name.Should().Be(command.Model.Name);
            updatedAuthor.Surname.Should().Be(command.Model.Surname);
            updatedAuthor.DateOfBirth.Should().Be(command.Model.DateOfBirth);
        }
    }
}