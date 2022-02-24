using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _context;
        public GetBooksQuery(BookStoreDbContext context)
        {
            this._context = context;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _context.Books.OrderBy(x => x.Id).ToList();
            List<BooksViewModel> vm = new();
            foreach (var book in bookList)
            {
                vm.Add(new BooksViewModel()
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PageCount = book.PageCount,
                    PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy")
                });
            }
            return vm;
        }
        public class BooksViewModel
        {
            public string Title { get; set; }
            public string Genre { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
        }
    }
}