using FluentAssertions;
using WebApi.Applications.GenreOperations.Command.DeleteGenre;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTest
    {
        #region WhenInvalidGenreIdIsGiven_Validator_ShouldBeReturnError()
        [Theory]
        [InlineData(-4)]
        [InlineData(-5)]
        [InlineData(-6)]
        public void WhenInvalidGenreIdIsGiven_Validator_ShouldBeReturnError(int genreId)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = genreId;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        #endregion

    }
}