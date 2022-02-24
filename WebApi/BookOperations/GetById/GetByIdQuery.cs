using System;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetById
{
    public class GetByIdQuery
    {
        private readonly BookStoreDbContext _context;

        public GetByIdQuery(BookStoreDbContext context)
        {
            _context = context;
        }
        public int BookId { get; set; }
        public BookDetailViewModel Handle()
        {
            var book = _context.Books.Where(x => x.Id == BookId).SingleOrDefault();
            if (book == null)
                throw new InvalidOperationException(BookId + " numaralı kitap bulunamadı.");

            BookDetailViewModel vm = new BookDetailViewModel();

            vm.Title = book.Title;
            vm.Genre = ((GenreEnum)book.GenreId).ToString();
            vm.PageCount = book.PageCount;
            vm.PublishDate = book.PublishDate.ToString("dd/MM/yyy");

            return vm;
        }

    }
    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}