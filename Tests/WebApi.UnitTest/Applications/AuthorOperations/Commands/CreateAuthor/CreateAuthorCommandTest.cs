using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTest.TestSetup;
using WebApi.Applications.AuthorOperations.Command.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommandTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        #region WhenAlreadyExistAuthorNameAndSurnameAreGiven_InvalidOperationException_ShouldBeReturn()

        [Fact]
        public void WhenAlreadyExistAuthorNameAndSurnameAreGiven_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange

            var author = new Author
            {
                Name = "Nikos",
                Surname = "Kazancakis",
                DateOfBirth = new DateTime(1900, 12, 10)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorModel()
            {
                Name = author.Name,
                Surname = author.Surname
            };

            //Act & Assert

            FluentActions.Invoking(
                () => command.Handle()
                    )
                    .Should().Throw<InvalidOperationException>()
                    .And.Message.Should().Be("Yazar zaten mevcut.");

        }
        #endregion

        #region WhenValidInputsAreGiven_Author_ShouldBeCreated()
        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            //Arrange

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorModel model = new CreateAuthorModel()
            {
                Name = "Ursula",
                Surname = "Le Guin",
                DateOfBirth = new DateTime(1919, 05, 21)
            };
            command.Model = model;

            //Act

            FluentActions.Invoking(
                () => command.Handle()
            ).Invoke();

            //Arrange

            var author = _context.Authors.First(x => x.Name == model.Name && x.Surname == model.Surname);

            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.DateOfBirth.Should().Be(model.DateOfBirth);

        }
        #endregion

    }
}