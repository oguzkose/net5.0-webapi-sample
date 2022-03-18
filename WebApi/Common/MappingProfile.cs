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
using WebApi.Applications.AuthorOperations.Command.CreateAuthor;
using static WebApi.Applications.UserOperations.Command.CreateUser.CreateUserCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Book Mapping Profile
            CreateMap<CreateBookModel, Book>();

            CreateMap<Book, BookDetailViewModel>()
            .ForMember(
                dest => dest.Genre,
                opt => opt
                .MapFrom(src => src.Genre.Name)

            )
            .ForMember(
                dest => dest.Author,
                opt => opt
                .MapFrom(src => src.Author.Name + " " + src.Author.Surname)
            )
            //Source' de ki DateTime PublishDate' i Destination' da ki string PublishDate' e map'leyen config.  
            .ForMember(
                dest => dest.PublishDate,
                opt => opt
                .MapFrom(src => (src.PublishDate.ToString("dd/MM/yyy")))
            );


            CreateMap<Book, BooksViewModel>()
            .ForMember(
                dest => dest.Genre,
                opt => opt
                .MapFrom(src => src.Genre.Name)
            )
            .ForMember(
                dest => dest.Author,
                opt => opt
                .MapFrom(src => src.Author.Name + " " + src.Author.Surname)
            )
            .ForMember(
                dest => dest.PublishDate,
                opt => opt
                .MapFrom(src => (src.PublishDate.ToString("dd/MM/yyy")))
            );
            #endregion

            #region Genre Mapping Profile
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateGenreModel, Genre>();
            #endregion

            #region Author Mapping Profile
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

            CreateMap<CreateAuthorModel, Author>();
            #endregion

            //User Mapping Profile
            CreateMap<CreateUserModel, User>();




        }

    }
}