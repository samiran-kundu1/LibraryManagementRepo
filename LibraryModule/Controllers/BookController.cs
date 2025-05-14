using LibraryModule.DTO;
using LibraryModule.Mapper;
using LibraryModule.Models;
using LibraryModule.Service;
using Microsoft.AspNetCore.Mvc;


namespace LibraryModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private IBookLibraryService _bookLibraryService;

        public BookController(IBookLibraryService bookLibraryService)
        {
                _bookLibraryService = bookLibraryService;
        }

        // POST: api/Library/AddBook
        [HttpPost("AddBook")]
        public IActionResult AddBook([FromBody] BookDTO book)
        {
            try
            {
                _bookLibraryService.AddBook(book.Title, book.Author);
                return Ok("Book added successfully.");
            }
            catch (InvalidOperationException inEx)
            {

                return Conflict(new { message = inEx.Message });
            }
        }



        // DELETE: api/Library/RemoveBook/{id}
        [HttpDelete("RemoveBook/{id}")]
        public IActionResult RemoveBook(int id)
        {
            var result = _bookLibraryService.RemoveBook(id);
            if (result)
                return Ok("Book removed successfully.");
            return NotFound("Book not found or currently borrowed.");
        }

        // GET: api/Library/SearchBooks?query=searchTerm
        [HttpGet("SearchBooks")]
        public IActionResult SearchBooks(string query)
        {
            var books = _bookLibraryService.SearchBooks(query);
            return Ok(books);
        }

        // POST: api/Library/RegisterUser
        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser([FromBody] UserDTO user)
        {
            _bookLibraryService.RegisterUser(user.Name);
            return Ok("User registered successfully.");
        }

        // POST: api/Library/BorrowBook
        [HttpPost("BorrowBook")]
        public IActionResult BorrowBook([FromBody] BorrowRequest request)
        {
            var result = _bookLibraryService.BorrowBook(request.UserId, request.BookId);
            if (result)
                return Ok("Book borrowed successfully.");
            return BadRequest("Unable to borrow book.");
        }

        // POST: api/Library/ReturnBook
        [HttpPost("ReturnBook")]
        public IActionResult ReturnBook([FromBody] BorrowRequest request)
        {
            var result = _bookLibraryService.ReturnBook(request.UserId, request.BookId);
            if (result)
                return Ok("Book returned successfully.");
            return BadRequest("Unable to return book.");
        }

        // GET: api/Library/GetBorrowedBooks/{userId}
        [HttpGet("BorrowedBooks/{userId}")]
        public IActionResult GetBorrowedBooks(int userId)
        {
            var books = _bookLibraryService.GetBorrowedBooks(userId);
            return Ok(books);
        }

        [HttpGet("Books")]
        public IActionResult GetAllBooks()
        {
            var books = _bookLibraryService.GetAllBooks();
            return Ok(books.MapToBooksDTOList());
        }
    }

}

