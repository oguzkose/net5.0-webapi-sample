using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Applications.GenreOperations.Command.UpdateGenre
{
    public class UpdateGenreCommand
    {
        private readonly BookStoreDbContext _context;

        public UpdateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        public void Handle()
        {
            var currentGenre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (currentGenre is null)
                throw new InvalidOperationException(GenreId + " numaralı güncellenecek Tür bulunamadı");

            currentGenre.Name = Model.Name != default ? Model.Name : currentGenre.Name;
            currentGenre.IsActive = Model.IsActive != default ? Model.IsActive : currentGenre.IsActive;

            _context.SaveChanges();
        }
    }
    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}