using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTest.TestSetup;
using WebApi.Applications.GenreOperations.Query.GetGenres;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.GenreOperations.Query.GetGenres
{
    public class GetGenresQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenresQueryTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }


        [Fact]
        public void WhenGenresIsEmpty_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var genres = _context.Genres.OrderBy(x => x.Id);
            _context.Genres.RemoveRange(genres);
            _context.SaveChanges();

            GetGenresQuery query = new GetGenresQuery(_context, null);

            //Act & Assert
            FluentActions.Invoking(
                () => query.Handle()
            ).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Tür bulunamadı");

        }
        [Fact]
        public void WhenGenresNotEmpty_Genres_ShouldBeReturn()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);


            var result = FluentActions.Invoking(
                () => query.Handle()
            ).Invoke();

            result.Should().NotBeEmpty();
        }
    }
}