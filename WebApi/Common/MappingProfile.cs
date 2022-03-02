using AutoMapper;
using WebApi.Applications.BookOperations.Command.CreateBook;
using WebApi.Applications.BookOperations.Query.GetBooks;
using WebApi.Applications.BookOperations.Query.GetBookDetail;
using WebApi.Applications.GenreOperations.Query.GetGenres;
using WebApi.Entities;
using WebApi.Applications.GenreOperations.Query.GetGenreDetail;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();

            #region CreateMap<Book, BookDetailViewModel>()
            CreateMap<Book, BookDetailViewModel>()
            //Source'de ki integer GenreId' yi Destination'da ki string Genre' ye (GenreEnum) map'lemyen config. 
            .ForMember(
                dest => dest.Genre,
                opt => opt
                .MapFrom(src => ((GenreEnum)src.GenreId)
                .ToString())
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
                .MapFrom(src => ((GenreEnum)src.GenreId)
                .ToString())
            )
            .ForMember(
                dest => dest.PublishDate,
                opt => opt
                .MapFrom(src => (src.PublishDate.ToString("dd/MM/yyy")))
            );
            #endregion

            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
        }

    }
}