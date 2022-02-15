using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class BookController : ControllerBase
    {
        public static List<Book> BookList = new List<Book>()
        {
            new Book{
                Id=1,
                Title="Nutuk",
                GenreId=1, //History
                PageCount=450,
                PublishDate= new DateTime(1937,05,13)
            },
            new Book{
                Id=2,
                Title="Şimdinin Gücü",
                GenreId=2, //philosophy
                PageCount=350,
                PublishDate= new DateTime(2018,12,01)

            },
            new Book{
                Id=3,
                Title="Dune",
                GenreId=3, //Fantastic
                PageCount=520,
                PublishDate= new DateTime(1989,02,15)
            }
        };



        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = BookList.OrderBy(x => x.Id).ToList();
            return bookList;
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = BookList.Where(x => x.Id == id).SingleOrDefault();
            return book;
        }
    }
}