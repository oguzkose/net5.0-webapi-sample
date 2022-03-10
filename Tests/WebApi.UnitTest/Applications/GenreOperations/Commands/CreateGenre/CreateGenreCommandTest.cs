using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Applications.GenreOperations.Command.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Tests.WebApi.UnitTest.TestSetup;
using Xunit;

namespace Tests.WebApi.UnitTest.Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenAllreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var genre = new Genre()
            {
                Name = "philosophy"
            };

            _context.Genres.Add(genre);
            _context.SaveChanges();


            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);

            command.Model = new CreateGenreModel()
            {
                Name = genre.Name
            };
            //Act & Assert
            FluentActions.Invoking(
                () => command.Handle()
            )
            .Should()
            .Throw<InvalidOperationException>().And.Message.Should().Be("Tür zaten mevcut");
        }


        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            //Arrange
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            CreateGenreModel model = new CreateGenreModel()
            {
                Name = "Romance"
            };

            command.Model = model;

            //Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert

            var genre = _context.Genres.First(x => x.Name == model.Name);

            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
            genre.IsActive.Should().Be(true);

        }

    }
}