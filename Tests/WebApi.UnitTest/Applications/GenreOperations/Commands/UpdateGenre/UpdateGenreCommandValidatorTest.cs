using FluentAssertions;
using Tests.WebApi.UnitTest.TestSetup;
using WebApi.Applications.GenreOperations.Command.UpdateGenre;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(-1, "Romance", true)]
        [InlineData(-2, "", false)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int genreId, string name, bool isActive)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = genreId;
            command.Model = new UpdateGenreModel()
            {
                Name = name,
                IsActive = isActive
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Theory]
        [InlineData(1, "Romance", true)]
        [InlineData(2, "Poetry", true)]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError(int genreId, string name, bool isActive)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = genreId;
            command.Model = new UpdateGenreModel()
            {
                Name = name,
                IsActive = isActive
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeLessThan(1);

        }
    }
}
