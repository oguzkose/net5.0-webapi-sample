using AutoMapper;
using WebApi.Applications.BookOperations.Command.CreateBook;
using WebApi.Applications.BookOperations.Query.GetBooks;
using WebApi.Applications.BookOperations.Query.GetBookDetail;
using WebApi.Applications.GenreOperations.Query.GetGenres;
using WebApi.Entities;
using WebApi.Applications.GenreOperations.Query.GetGenreDetail;
using WebApi.Applications.GenreOperations.Command.CreateGenre;
using WebApi.Applications.AuthorOperations.Query.GetAuthors;
using WebApi.Applications.AuthorOperations.Query.GetAuthorDetail;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();

            #region CreateMap<Book, BookDetailViewModel>()
            CreateMap<Book, BookDetailViewModel>()
            .ForMember(
                dest => dest.Genre,
                opt => opt
                .MapFrom(src => src.Genre.Name)

            )
            //Source' de ki DateTime PublishDate' i Destination' da ki string PublishDate' e map'leyen config.  
            .ForMember(
                dest => dest.PublishDate,
                opt => opt
                .MapFrom(src => (src.PublishDate.ToString("dd/MM/yyy")))
            );
            #endregion

            #region CreateMap<Book, BooksViewModel>()
            CreateMap<Book, BooksViewModel>()
            .ForMember(
                dest => dest.Genre,
                opt => opt
                .MapFrom(src => src.Genre.Name)
            )
            .ForMember(
                dest => dest.PublishDate,
                opt => opt
                .MapFrom(src => (src.PublishDate.ToString("dd/MM/yyy")))
            );
            #endregion

            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateGenreModel, Genre>();

            CreateMap<Author, AuthorViewModel>()
            .ForMember(
                dest => dest.DateOfBirth,
                opt => opt
                .MapFrom(src => src.DateOfBirth.ToShortDateString())
            );

            CreateMap<Author, AuthorDetailViewModel>()
            .ForMember(
                dest => dest.DateOfBirth,
                opt => opt
                .MapFrom(
                    src => src.DateOfBirth.ToShortDateString()
                )
            );
        }

    }
}