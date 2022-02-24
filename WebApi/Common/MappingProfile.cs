using AutoMapper;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.GetById;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();


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
        }

    }
}