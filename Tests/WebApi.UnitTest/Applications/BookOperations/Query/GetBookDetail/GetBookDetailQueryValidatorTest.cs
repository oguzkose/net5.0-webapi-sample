using FluentAssertions;
using WebApi.Applications.BookOperations.Query.GetBookDetail;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.BookOperations.Query.GetBookDetail
{
    public class GetBookDetailQueryValidatorTest
    {
        #region WhenInvalidBookIdIsGiven_Validator_ShouldBeReturnError()
        [Theory]
        [InlineData(-5)]
        [InlineData(0)]
        public void WhenInvalidBookIdIsGiven_Validator_ShouldBeReturnError(int bookId)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = bookId;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);

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
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = bookId;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeLessThan(1);
        }
        #endregion
    }
}