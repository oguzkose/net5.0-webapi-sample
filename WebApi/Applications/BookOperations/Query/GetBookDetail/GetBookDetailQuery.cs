using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Applications.BookOperations.Query.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int BookId { get; set; }
        public BookDetailViewModel Handle()
        {
            var book = _context.Books
                .Include(x => x.Genre)
                .Include(y => y.Author)
                .Where(z => z.Id == BookId).SingleOrDefault();

            if (book == null)
                throw new InvalidOperationException(BookId + " numaralı kitap bulunamadı.");

            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);

            return vm;
        }

    }
    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}