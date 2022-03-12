using System;
using System.Linq;
using FluentAssertions;
using Tests.WebApi.UnitTest.TestSetup;
using WebApi.Applications.GenreOperations.Command.UpdateGenre;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateGenreCommandTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }


        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        public void WhenUnavailableGenreIdIsGiven_InvalidOperationException_ShouldBeReturn(int genreId)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = genreId;

            FluentActions.Invoking(
                () => command.Handle()
            ).Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be(genreId + " numaralı güncellenecek Tür bulunamadı");

        }

        [Theory]
        [InlineData(1, "Science Fiction")]
        [InlineData(2, "History")]
        [InlineData(3, "Personal Growth")]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn(int genreId, string genreName)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = genreId;
            command.Model = new UpdateGenreModel()
            {
                Name = genreName,
                IsActive = true
            };

            FluentActions.Invoking(
                () => command.Handle()
            ).Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Aynı isimli bir Kitap Türü zaten mevcut");

        }

        [Theory]
        [InlineData(1, "Poetry")]
        [InlineData(2, "Biography")]
        [InlineData(3, "Romance")]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated(int genreId, string genreName)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = genreId;
            command.Model = new UpdateGenreModel()
            {
                Name = genreName,
                IsActive = true
            };

            FluentActions.Invoking(
                () => command.Handle()
            ).Invoke();

            var genre = _context.Genres.First(x => x.Id == genreId);

            genre.Name.Should().Be(genreName);
        }
    }
}
//TODO update genre command happy path testini yap