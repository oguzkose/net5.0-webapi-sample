using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTest.TestSetup;
using WebApi.Applications.AuthorOperations.Query.GetAuthorDetail;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.AuthorOperations.Query.GetAuthorDetail
{
    public class GetAuthorDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorDetailQueryTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }


        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        public void WhenUnavailableAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn(int authorId)
        {
            //Arrange

            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, null);
            query.AuthorId = authorId;

            //Act & Assert

            FluentActions.Invoking(
                () => query.Handle()
            ).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı");

        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenValidAuthorIdIsGiven_Author_ShouldBeReturn(int authorId)
        {
            //Arrange

            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = authorId;

            //Act

            var result = FluentActions.Invoking(
                () => query.Handle()
            ).Invoke();

            //Assert

            var author = _context.Authors.First(x => x.Id == authorId);
            result.Name.Should().Be(author.Name);
            result.Surname.Should().Be(author.Surname);
            result.DateOfBirth.Should().Be(author.DateOfBirth.ToShortDateString());

        }
    }
}