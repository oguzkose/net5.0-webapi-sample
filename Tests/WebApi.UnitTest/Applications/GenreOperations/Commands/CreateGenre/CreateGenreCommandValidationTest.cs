using FluentAssertions;
using WebApi.Applications.GenreOperations.Command.CreateGenre;
using Tests.WebApi.UnitTest.TestSetup;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidationTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            //Arrange

            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.Model = new CreateGenreModel()
            {
                Name = name,
            };

            //Act

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //Assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        //Happy Path

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            //Arrange

            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.Model = new CreateGenreModel()
            {
                Name = "Philosophy"
            };

            //Act

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //Assert

            result.Errors.Count.Should().Be(0);
        }
    }
}