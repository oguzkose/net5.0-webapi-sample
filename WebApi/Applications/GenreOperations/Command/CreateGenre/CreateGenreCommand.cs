using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.GenreOperations.Command.CreateGenre
{

    public class CreateGenreCommand
    {

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateGenreCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public CreateGenreModel Model { get; set; }
        public void Handle()
        {
            var genre = _context.Genres.FirstOrDefault(x => x.Name == Model.Name);
            if (genre is not null)
                throw new InvalidOperationException("Tür zaten mevcut");

            genre = _mapper.Map<Genre>(Model);
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }

    }
    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}