using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Applications.GenreOperations.Command.DeleteGenre
{
    public class DeleteGenreCommand
    {
        private readonly BookStoreDbContext _context;

        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public int GenreId { get; set; }
        public void Handle()
        {
            var deletedGenre =  _context.Genres.FirstOrDefault(x=>x.Id == GenreId);
            if(deletedGenre is null)
                throw new InvalidOperationException("Silinecek Tür bulunamadı");
            _context.Genres.Remove(deletedGenre);
            _context.SaveChanges();    
        }

    }

}