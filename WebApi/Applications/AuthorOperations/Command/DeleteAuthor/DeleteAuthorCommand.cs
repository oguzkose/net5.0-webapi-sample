using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Applications.AuthorOperations.Command.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _context;

        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public int AuthorId { get; set; }
        public void Handle()
        {

            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Silinecek yazar bulunamadı");


            var booksOfAuthor = _context.Books.Where(x => x.AuthorId == AuthorId).ToList();
            if (booksOfAuthor.Count > 0)
                _context.Books.RemoveRange(booksOfAuthor);
    
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }

    }

}