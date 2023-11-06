using Microsoft.AspNetCore.Mvc;
using REST.Core.Models;

namespace REST.Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibraryUsersController : ControllerBase
    {
        //static repository
        private static List<LibraryUser> _libraryUser = new List<LibraryUser>
        {
            new LibraryUser { Id = 1, Name = "John Doe", Email = "xyz@seznam.cz", DateJoined = DateTime.Parse("2022-08-15T09:34:10.0000000Z"), BorrowedBooks = new List<int>()},
            new LibraryUser { Id = 2, Name = "Jane Smith", Email = "janeSm@email.cz", DateJoined = DateTime.Parse("2023-10-23T13:28:30.0000000Z"), BorrowedBooks = new List<int>{1}}
        };

        private readonly ILogger<LibraryUsersController> _logger;

        public LibraryUsersController(ILogger<LibraryUsersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LibraryUser>> GetLibraryUsers()
        {
            return Ok(_libraryUser); // 200 OK
        }

        [HttpGet("{id}")]
        public ActionResult<LibraryUser> GetLibraryUser(int id)
        {
            var libraryUser = _libraryUser.FirstOrDefault(b => b.Id == id);

            if (libraryUser == null)
            {
                return NotFound(); // 404 Not Found
            }

            return Ok(libraryUser); // 200 OK
        }

        [HttpPost]
        public ActionResult<LibraryUser> PostLibraryUser(LibraryUser libraryUser)
        {
            var maxId = _libraryUser.Max(b => b.Id);
            libraryUser.Id = maxId + 1;
            _libraryUser.Add(libraryUser);

            return CreatedAtAction(nameof(GetLibraryUser), new { id = libraryUser.Id }, libraryUser); // 201 Created
        }

        [HttpPut("{id}")]
        public ActionResult PutLibraryUser(int id, LibraryUser updatedLibraryUser)
        {
            var libraryUser = _libraryUser.FirstOrDefault(b => b.Id == id);
            
            if (libraryUser == null)
            {
                return NotFound(); // 404 Not Found
            }

            libraryUser.Name = updatedLibraryUser.Name;
            libraryUser.Email = updatedLibraryUser.Email;
            libraryUser.DateJoined = updatedLibraryUser.DateJoined;
            libraryUser.BorrowedBooks = updatedLibraryUser.BorrowedBooks;

            return Ok(libraryUser); // 200 OK
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteLibraryUser(int id)
        {
            var libraryUser = _libraryUser.FirstOrDefault(b => b.Id == id);
            
            if (libraryUser == null)
            {
                return NotFound(); // 404 Not Found
            }

            _libraryUser.Remove(libraryUser);

            return NoContent(); // 204 No Content
        }
    }
}