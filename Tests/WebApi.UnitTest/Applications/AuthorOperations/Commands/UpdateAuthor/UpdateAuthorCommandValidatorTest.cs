using System;
using FluentAssertions;
using Tests.WebApi.UnitTest.TestSetup;
using WebApi.Applications.AuthorOperations.Command.UpdateAuthor;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        #region WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
        [Theory]
        [InlineData(1, "", "")]
        [InlineData(1, "  ", "  ")]
        [InlineData(1, "Ursula", "")]
        [InlineData(1, "", "Le Guin")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int authorId, string name, string surname)
        {
            //Arrange

            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = authorId;
            command.Model = new UpdateAuthorModel()
            {
                Name = name,
                Surname = surname,
                DateOfBirth = DateTime.Now.Date
            };

            //Act

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            //Assert

            result.Errors.Count.Should().BeGreaterThan(0);

        }
        #endregion

        #region WhenDateTimeEqualNowIsGiven_Validator_ReturnShouldBeError()
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ReturnShouldBeError()
        {
            //Arrange

            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel()
            {
                Name = "Ursula",
                Surname = "Le Guin",
                DateOfBirth = DateTime.Now.Date
            };

            //Act

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            //Assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        #endregion

        #region WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //Arrange

            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel()
            {
                Name = "Ursula",
                Surname = "Le Guin",
                DateOfBirth = new DateTime(1919, 01, 01)
            };

            //Act

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            //Assert

            result.Errors.Count.Should().Be(0);
        }
        #endregion
    }
}