using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTest.TestSetup;
using WebApi.Applications.AuthorOperations.Query.GetAuthors;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.AuthorOperations.Query.GetAuthors
{
    public class GetAuthorsQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorsQueryTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        #region WhenThereIsNoCurrentAuthor_InvalidOperationException_ShouldBeReturn()
        [Fact]
        public void WhenThereIsNoCurrentAuthor_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange

            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);

            var authorList = _context.Authors.ToList();
            _context.RemoveRange(authorList);
            _context.SaveChanges();

            // Act & Assert

            FluentActions.Invoking(
                () => query.Handle()
            ).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kayıtlı bir yazar bulunamadı");

        }
        #endregion

        #region WhenThereAreCurrentAuthor_Authors_ShouldBeReturn()
        [Fact]
        public void WhenThereAreCurrentAuthor_Authors_ShouldBeReturn()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);



            var result = FluentActions.Invoking(
                () => query.Handle()
            ).Invoke();

            result.Count.Should().BeGreaterThan(0);
        }
        #endregion

    }
}