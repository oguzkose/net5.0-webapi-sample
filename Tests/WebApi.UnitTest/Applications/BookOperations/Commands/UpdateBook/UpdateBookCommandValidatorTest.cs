using System;
using FluentAssertions;
using WebApi.Applications.BookOperations.Command.UpdateBook;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTest
    {
        #region WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()

        [Theory]
        [InlineData(-1, "    ", -5, -2, -1)]
        [InlineData(0, "", 0, 0, 0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId, string title, int authorId, int genreId, int pageCount)
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = bookId;
            command.Model = new UpdatedBookModel()
            {
                Title = title,
                AuthorId = authorId,
                GenreId = genreId,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(5);
        }
        #endregion

        #region WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = 1;
            command.Model = new UpdatedBookModel()
            {
                Title = "Zorba",
                AuthorId = 1,
                GenreId = 1,
                PageCount = 275,
                PublishDate = DateTime.Now.Date
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);

        }
        #endregion

        #region WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        [Theory]
        [InlineData(1, "Zorba", 1, 2, 500)]
        [InlineData(2, "Incognito", 2, 3, 700)]
        [InlineData(3, "Hobbit", 3, 3, 800)]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError(int bookId, string title, int authorId, int genreId, int pageCount)
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = bookId;
            command.Model = new UpdatedBookModel()
            {
                Title = title,
                AuthorId = authorId,
                GenreId = genreId,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-15)
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().Be(true);
        }
        #endregion

    }
}