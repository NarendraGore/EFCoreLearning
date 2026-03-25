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

        [HttpPost("bulk")]

        public async Task<IActionResult> AddBooks([FromBody] List<Book> model)
        {
            _appDbContext.Books.AddRange(model);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}