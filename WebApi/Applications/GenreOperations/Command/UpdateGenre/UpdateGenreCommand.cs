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
            if (_context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
                throw new InvalidOperationException("Aynı isimli bir Kitap Türü zaten mevcut");

            currentGenre.Name = String.IsNullOrEmpty(Model.Name.Trim()) ? currentGenre.Name : Model.Name;
            currentGenre.IsActive = Model.IsActive;

            _context.SaveChanges();
        }
    }
    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}