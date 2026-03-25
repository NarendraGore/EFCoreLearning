using EFCoreLearning.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public BookController(AppDbContext appDbContext)
        {

            _appDbContext = appDbContext;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllBook()
        {
            var result = await _appDbContext.Books.ToListAsync();
            return Ok(result);
        }

        [HttpPost("")]

        public async Task<IActionResult> AddBookAsync([FromBody] Book model)
        {
            model.CreatedOn = DateTime.UtcNow;
            _appDbContext.Books.Add(model);
            await _appDbContext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPost("single")]

        public async Task<IActionResult> AddBooks([FromBody] Book model)
        {

            //var author = new Author()
            //{
            //    Name="Test1",
            //    Email = "test1@gmail.com"

            //};
            //model.Author= author;
            _appDbContext.Books.Add(model);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookById([FromRoute] int id, [FromBody] Book model) {
            var book = _appDbContext.Books.FirstOrDefault(x => x.Id == id);
            if (book == null) {
                return NotFound();

            }

            book.Title = model.Title;
            book.AuthorId = model.AuthorId;
            book.NoOfPages = model.NoOfPages;
            book.Description = model.Description;

            await _appDbContext.SaveChangesAsync();

            return Ok(book);
        }
    }
}