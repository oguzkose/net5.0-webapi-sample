using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.GetById;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class BookController : ControllerBase
    {
        #region Injections
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region GetBooks(GET)

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
        }
        #endregion

        #region GetById(GET)

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetByIdQuery query = new GetByIdQuery(_context,_mapper);
                query.BookId = id;
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }
        #endregion

        #region CreateBook(POST)

        [HttpPost]
        public IActionResult CreateBook([FromBody] CreateBookModel newBook)
        {

            try
            {
                CreateBookCommand command = new CreateBookCommand(_context, _mapper);
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Kitap başarıyla eklendi !");
        }
        #endregion

        #region UpdateBook(PUT)

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdatedBookModel updatedBook)
        {

            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.Model = updatedBook;
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok("Kitap güncellendi");
        }
        #endregion

        #region DeleteBook(DELETE)
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {

            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(id + " numaralı kitap silindi");
        }
        #endregion

    }
}