using System;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTest.TestSetup;
using WebApi.Applications.BookOperations.Query.GetBookDetail;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.BookOperations.Query.GetBookDetail
{
    public class GetBookDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        #region WhenUnAvailableBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        [Theory]
        [InlineData(6)]
        [InlineData(7)]
        public void WhenUnAvailableBookIdIsGiven_InvalidOperationException_ShouldBeReturn(int bookId)
        {
            //Arrange

            GetBookDetailQuery query = new GetBookDetailQuery(_context, null);
            query.BookId = bookId;

            //Act & Assert

            FluentActions.Invoking(
                () => query.Handle()
            ).Should().Throw<InvalidOperationException>().And.Message.Should().Be(bookId + " numaralı kitap bulunamadı.");

        }
        #endregion

        #region WhenValidBookIdIsGiven_Book_ShouldBeReturn(int bookId)
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenValidBookIdIsGiven_Book_ShouldBeReturn(int bookId)
        {
            //Arrange

            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = bookId;

            //Act

            var result = FluentActions.Invoking(
                () => query.Handle()
            ).Invoke();

            //Assert

            result.Should().NotBeNull();
        }
        #endregion

    }
}