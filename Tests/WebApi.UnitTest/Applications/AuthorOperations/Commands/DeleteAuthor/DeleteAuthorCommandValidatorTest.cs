using FluentAssertions;
using WebApi.Applications.AuthorOperations.Command.DeleteAuthor;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTest
    {
        #region WhenInvalidAuthorIdIsGiven_Validator_ShouldBeReturnError()
        [Theory]
        [InlineData(-5)]
        public void WhenInvalidAuthorIdIsGiven_Validator_ShouldBeReturnError(int authorId)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.AuthorId = authorId;

            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        #endregion
    }
}