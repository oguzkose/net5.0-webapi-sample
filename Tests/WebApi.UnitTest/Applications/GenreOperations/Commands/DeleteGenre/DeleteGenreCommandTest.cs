using System;
using System.Linq;
using FluentAssertions;
using Tests.WebApi.UnitTest.TestSetup;
using WebApi.Applications.GenreOperations.Command.DeleteGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommandTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Theory]
        [InlineData(15)]
        [InlineData(16)]
        [InlineData(17)]
        public void WhenUnavailableGenreIdIsGiven_InvalidOperationException_ShouldBeReturn(int genreId)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = genreId;

            FluentActions.Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek Tür bulunamadı");

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenValidGenreIdIsGiven_Genre_ShouldBeDeleted(int genreId)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = genreId;

            FluentActions.Invoking(
                () => command.Handle()
            ).Invoke();

            var result = _context.Genres.FirstOrDefault(x => x.Id == genreId);
            result.Should().BeNull();
        }

        
    }
}