using System.Linq;
using WebApi.DBOperations;
using System;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public int BookId { get; set; }
        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
                throw new InvalidOperationException(BookId + " numaralı silinecek kitap bulunamadı");

            _context.Books.Remove(book);
            _context.SaveChanges();
        }

    }
}