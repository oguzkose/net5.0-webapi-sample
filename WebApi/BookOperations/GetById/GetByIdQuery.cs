using System;
using System.Linq;
using AutoMapper;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetById
{
    public class GetByIdQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetByIdQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int BookId { get; set; }
        public BookDetailViewModel Handle()
        {
            var book = _context.Books.Where(x => x.Id == BookId).SingleOrDefault();
            if (book == null)
                throw new InvalidOperationException(BookId + " numaralı kitap bulunamadı.");

            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);
           
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