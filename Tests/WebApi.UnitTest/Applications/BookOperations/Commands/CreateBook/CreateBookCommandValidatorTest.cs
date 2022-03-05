using System;
using FluentAssertions;
using WebApi.Applications.BookOperations.Command.CreateBook;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Applications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        #region WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId)

        // Input'lara Valid olmayan değerler verilirse Error dönmeli
        [Theory]
        [InlineData("Lord Of The Rings", 0, 0)]
        [InlineData("Lord Of The Rings", 1, 0)]
        [InlineData("Lord Of The Rings", 0, 1)]
        [InlineData("", 0, 0)]
        [InlineData("", 1, 0)]
        [InlineData("", 0, 1)]
        [InlineData("", 1, 1)]
        [InlineData("       ", 1, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
        {
            //Arrange

            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId
            };

            //Act

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //Assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        #endregion

        #region WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        //Eğer PublishDate DateTime.Now verilirse Error dönmeli 
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            //Arrange

            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 1000,
                GenreId = 1,
                PublishDate = DateTime.Now.Date
            };

            //Act

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //Assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        #endregion

        #region Happy Path - WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()

        //Eğer tüm inputlar valid ise Error dönmemeli
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //Arrange

            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Hobbit",
                PageCount = 2000,
                GenreId = 2,
                PublishDate = DateTime.Now.Date.AddYears(-1)
            };

            //Act

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().Be(0);
        }
        #endregion

    }
}