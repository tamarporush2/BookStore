using BookStore.Services;
using Microsoft.AspNetCore.Mvc;


namespace BookStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/<Books>
        [HttpGet]
        public ActionResult<List<Book>> Get()
        {
            try
            {
                return Ok(_bookService.GetAllBooks());
            }
            catch 
            {               
                return StatusCode(500);
            }
        }

        // GET api/<Books>/5
        [HttpGet("{isbn}")]
        public ActionResult<Book> Get(string isbn)
        {
            try
            {
                return Ok(_bookService.GetBookByIsbn(isbn));
            }
            catch (Exception ex)
            {
                if (ex is KeyNotFoundException)
                    return NotFound(ex.Message);
                return StatusCode(500);
            }
        }

        // POST api/<Books>
        [HttpPost]
        public ActionResult Post([FromBody] Book book)
        {
            try
            {
                _bookService.AddBook(book);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                if (ex is KeyNotFoundException)
                    return NotFound(ex.Message);
                return StatusCode(500);
            }
        }

        // PUT api/<Books>/5
        [HttpPut("{isbn}")]
        public ActionResult Put(string isbn, [FromBody] Book book)
        {
            try
            {
                _bookService.UpdateBook(isbn, book);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                if (ex is KeyNotFoundException)
                    return NotFound(ex.Message);
                return StatusCode(500);
            }
        }

        // DELETE api/<Books>/5
        [HttpDelete("{isbn}")]
        public ActionResult Delete(string isbn)
        {
            try
            {
                _bookService.DeleteBook(isbn);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex is KeyNotFoundException)
                    return NotFound(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("report")]
        public ActionResult GetHtmlReport()
        {
            try
            {           
                return new ContentResult
                {
                    Content = _bookService.GetHtmlReport(),
                    ContentType = "text/html",
                    StatusCode = 200
                };
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
