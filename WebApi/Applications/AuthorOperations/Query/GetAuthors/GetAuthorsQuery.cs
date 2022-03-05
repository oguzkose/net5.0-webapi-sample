using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Applications.AuthorOperations.Query.GetAuthors
{

    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorViewModel> Handle()
        {
            var authorList = _context.Authors.ToList();

            if (authorList is null)
                throw new InvalidOperationException("Kayıtlı bir yazar bulunamadı");

            List<AuthorViewModel> vm = _mapper.Map<List<AuthorViewModel>>(authorList);
            return vm;
        }



    }
    public class AuthorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }
    }
}