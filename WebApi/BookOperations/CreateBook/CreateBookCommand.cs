using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        private readonly BookStoreDbContext _context;
        public CreateBookCommand(BookStoreDbContext context)
        {
            this._context = context;
        }
        public CreateBookModel Model { get; set; }
        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null)
                throw new InvalidOperationException("Kitap zaten mevcut");
            book = new Book()
            {
                Title = Model.Title,
                GenreId = Model.GenreId,
                PageCount = Model.PageCount,
                PublishDate = Model.PublishDate
            };


            _context.Books.Add(book);
            _context.SaveChanges();
        }

    }
    public class CreateBookModel
    {

        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}