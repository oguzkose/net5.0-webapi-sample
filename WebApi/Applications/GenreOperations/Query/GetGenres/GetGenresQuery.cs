using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Applications.GenreOperations.Query.GetGenres
{

    public class GetGenresQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenresQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<GenresViewModel> Handle()
        {
            var genres = _context.Genres.Where(x => x.IsActive == true).OrderBy(x => x.Id);
            if (genres is null)
                throw new InvalidOperationException();

            List<GenresViewModel> vm = _mapper.Map<List<GenresViewModel>>(genres);

            return vm;
        }


    }
    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}