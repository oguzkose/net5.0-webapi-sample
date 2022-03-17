using FluentAssertions;
using WebApi.Applications.GenreOperations.Query.GetGenreDetail;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.GenreOperations.Query.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTest
    {
        #region WhenInvalidGenreIdIsGiven_Validator_ShouldBeReturnError()
        [Theory]
        [InlineData(-5)]
        [InlineData(-10)]
        public void WhenInvalidGenreIdIsGiven_Validator_ShouldBeReturnError(int genreId)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            query.GenreId = genreId;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        #endregion

        #region WhenValidGenreIdIsGiven_Validator_ShouldBeNotReturnError()
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenValidGenreIdIsGiven_Validator_ShouldBeNotReturnError(int genreId)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            query.GenreId = genreId;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeLessThan(1);
        }
        #endregion

    }
}