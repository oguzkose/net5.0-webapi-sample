using System;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTest.TestSetup;
using WebApi.Applications.GenreOperations.Query.GetGenreDetail;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.GenreOperations.Query.GetGenreDetail
{
    public class GetGenreDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetailQueryTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Theory]
        [InlineData(14)]
        [InlineData(15)]
        public void WhenUnavailableGenreIdIsGiven_InvalidOperationException_ShouldBeReturn(int genreId)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, null);
            query.GenreId = genreId;

            FluentActions.Invoking(
                () => query.Handle()
            ).Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be(genreId + " numaralı tür bulunamadı");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenCurrentGenreIdIsGiven_Genre_ShouldBeReturn(int genreId)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = genreId;

            var result = FluentActions.Invoking(
                () => query.Handle()
            ).Invoke();

            result.Should().NotBeNull();
            result.Name.Should().NotBeNull();
        }

    }
}