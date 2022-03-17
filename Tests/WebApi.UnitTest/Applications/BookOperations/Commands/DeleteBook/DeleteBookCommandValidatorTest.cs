using FluentAssertions;
using WebApi.Applications.BookOperations.Command.DeleteBook;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTest
    {
        #region Name WhenInvalidBookIdIsGiven_Validator_ShouldBeReturnError()
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void WhenInvalidBookIdIsGiven_Validator_ShouldBeReturnError(int bookId)
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = bookId;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        #endregion

        #region WhenValidBookIdIsGiven_Validator_ShouldNotBeReturnError()

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenValidBookIdIsGiven_Validator_ShouldNotBeReturnError(int bookId)
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = bookId;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
        #endregion

    }
}