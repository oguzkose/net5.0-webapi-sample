using FluentAssertions;
using Tests.WebApi.UnitTest.TestSetup;
using WebApi.Applications.AuthorOperations.Query.GetAuthorDetail;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.AuthorOperations.Query.GetAuthorDetail
{
    public class GetAuthorDetailValidatorTest : IClassFixture<CommonTestFixture>
    {

        #region WhenInvalidAuthorIdIsGiven_Validator_ShouldBeReturnError(int authorId)
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAuthorIdIsGiven_Validator_ShouldBeReturnError(int authorId)
        {
            //Arrange

            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null, null);
            query.AuthorId = authorId;

            //Act

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(query);

            //Assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        #endregion

        #region WhenValidAuthorIdIsGiven_Validator_ShoulNotBeReturnError(int authorId)
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenValidAuthorIdIsGiven_Validator_ShoulNotBeReturnError(int authorId)
        {
            //Arrange
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null, null);
            query.AuthorId = authorId;

            //Act
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(query);

            //Assert
            result.Errors.Count.Should().Be(0);
        }
        #endregion

    }
}