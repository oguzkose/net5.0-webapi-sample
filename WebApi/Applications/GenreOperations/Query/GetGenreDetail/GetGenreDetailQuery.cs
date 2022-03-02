using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Applications.GenreOperations.Query.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQuery(BookStoreDbContext context, IMapper mapper = null)
        {
            _context = context;
            _mapper = mapper;
        }

        public int GenreId { get; set; }
        public GenreDetailViewModel Handle()
        {
            var genre = _context.Genres.FirstOrDefault(x => x.Id == GenreId);
            if (genre is null)
                throw new InvalidOperationException(GenreId + " numaralı tür bulunamadı");

            GenreDetailViewModel vm = _mapper.Map<GenreDetailViewModel>(genre);

            return vm;
        }
    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

}