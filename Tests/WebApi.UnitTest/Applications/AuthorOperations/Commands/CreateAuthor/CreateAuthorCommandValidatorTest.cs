using System;
using FluentAssertions;
using Tests.WebApi.UnitTest.TestSetup;
using WebApi.Applications.AuthorOperations.Command.CreateAuthor;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("", "")]
        [InlineData("  ", "  ")]
        [InlineData("Ursula", "")]
        [InlineData("", "Le Guin")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
        {
            //Arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = name,
                Surname = surname,
                DateOfBirth = new DateTime(1919, 01, 01)
            };

            //Act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ReturnShouldBeError()
        {
            //Arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = "Ursula",
                Surname = "Le Guin",
                DateOfBirth = DateTime.Now.Date
            };

            //Act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        //Happy Path

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = "Ursula",
                Surname = "Le Guin",
                DateOfBirth = new DateTime(1919, 01, 01)
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}