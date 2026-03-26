using System.Reflection.Metadata.Ecma335;
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


        [HttpPost("bulk")]

        public async Task<IActionResult> PostBulkData([FromBody] List<Book> model) {

            _appDbContext.Books.AddRange(model);
            await _appDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{BookId}")]

        public async Task<IActionResult> DeleteById([FromRoute] int BookId) {
            var book = new Book { Id = BookId };
            _appDbContext.Entry(book).State = EntityState.Deleted;
            await _appDbContext.SaveChangesAsync();

            //var result = await _appDbContext.Books.FirstOrDefaultAsync(x => x.Id == BookId);
            //if (result == null) {

            //    return NotFound();
            //}
            //_appDbContext.Books.Remove(result);
            //await _appDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("Delete-Books")]
        public async Task<IActionResult> DeleteBookInBulkAsync() {

            var book = await _appDbContext.Books.Where(x => x.Id <= 3).ToListAsync();
             _appDbContext.Books.RemoveRange(book);

            await _appDbContext.SaveChangesAsync();

            return Ok(); 
                }
        //Bulk Delete By Creating seprate OBJ and taking list of ids from Body

        [HttpDelete("Bulk-Delete")]

        public async Task<IActionResult> BulkDelete([FromBody] List<int> ids)
        {


            foreach (var Bookid in ids)
            {
                var book = new Book { Id = Bookid };
                _appDbContext.Books.Remove(book);
            }

            await _appDbContext.SaveChangesAsync();
            return Ok();
        }

        //Bulk Delete By using ExecuteDeleteAsync() Method;

        [HttpDelete("BulkDelete")]
        public async Task<IActionResult> DeleteBooksInBulkAsync() {

            var books = await _appDbContext.Books.Where(x => x.Id < 7).ExecuteDeleteAsync();
            return Ok();
        }
    }
}