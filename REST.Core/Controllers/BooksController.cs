using Microsoft.AspNetCore.Mvc;
using REST.Core.Models;

namespace REST.Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        //static repository
        internal static List<Book> _books = new List<Book>
        {
            new Book { Id = 1, Title = "The Bells", Author = "E. A. Poe", Year = 2001, ISBN = "978-3-16-148410-0", BorrowedByUserId = null},
            new Book { Id = 2, Title = "Fairy Tale", Author = "Stephen King", Year = 2002, ISBN = "191-1-52-359781-3", BorrowedByUserId = 2}
        };

        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return Ok(_books); // 200 OK
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            
            if (book == null)
            {
                return NotFound(); // 404 Not Found
            }

            return Ok(book); // 200 OK
        }

        [HttpPost]
        public ActionResult<Book> PostBook(Book book)
        {
            var maxId = _books.Max(b => b.Id);
            book.Id = maxId + 1;
            _books.Add(book);

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book); // 201 Created
        }

        [HttpPut("{id}")]
        public ActionResult PutBook(int id, Book updatedBook)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound(); // 404 Not Found
            }

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Year = updatedBook.Year;

            return Ok(book); // 200 OK
        }

        [HttpPut("{id}/libraryUser/{libraryUserId}")]   //todo - tobe discussed - according to task description, why this should be POST when editing existing object (not creating new one)?
        public ActionResult<Book> BorrowBook(int id, int libraryUserId)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound(); // 404 Not Found
            }

            var libraryUser = LibraryUsersController._libraryUser.FirstOrDefault(b => b.Id == libraryUserId);

            if (libraryUser == null)
            {
                return NotFound(); // 404 Not Found
            }

            book.BorrowedByUserId = libraryUser.Id;


            return Ok(book); // 200 OK
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound(); // 404 Not Found
            }

            _books.Remove(book);
            return NoContent(); // 204 No Content
        }
    }
}