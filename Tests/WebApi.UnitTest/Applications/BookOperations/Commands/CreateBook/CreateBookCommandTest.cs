using System;
using AutoMapper;
using FluentAssertions;
using WebApi.Applications.BookOperations.Command.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Applications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommandTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        #region WhenAllreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        //Varolan Title'da bir kitap yaratılmak istenirse InvalidOperationException dönmeli
        [Fact]
        public void WhenAllreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange

            var book = new Book()
            {
                Title = "WhenAllreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                PageCount = 1000,
                PublishDate = new DateTime(1992, 04, 21),
                GenreId = 1,

            };
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel()
            {
                Title = book.Title
            };

            //Act & Assert

            FluentActions
                .Invoking(
                    () => command.Handle()
                )
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kitap zaten mevcut");

        }
        #endregion

        
    }
}