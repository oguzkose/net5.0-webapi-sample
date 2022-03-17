using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Applications.BookOperations.Query.GetBooks
{
    public class GetBooksQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBooksQuery(IBookStoreDbContext context, IMapper mapper)
        {
            this._context = context;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _context.Books
                .Include(x => x.Genre)
                .Include(y => y.Author)
                .OrderBy(z => z.Id)
                .ToList();

            if (bookList.Count == 0)
                throw new InvalidOperationException("Kitap bulunamadı");

            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList);

            return vm;
        }

    }
    public class BooksViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}